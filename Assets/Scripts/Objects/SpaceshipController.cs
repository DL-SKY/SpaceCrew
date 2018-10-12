using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    #region Variables
    public string model;
    public string material;
    public float length;

    [Header("Targets")]
    public Transform targetMovePoint;
    public Transform targetFollow;
    public Transform targetOrbit;

    [Header("Main Renderer")]
    public MeshFilter mainFilter;
    public MeshRenderer mainRenderer;

    [Header("Camera")]
    public SpaceshipCameraPlace cameraPlace;

    private Rigidbody rb;

    private SpaceshipData data;
    private SpaceshipsConfig config;
    private SpaceshipMetadata meta;
    #endregion

    #region Unity methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Start()
    {
        //TODO, пока тестирование
        InitializeSpaceship();
    }
    #endregion

    #region Public methods
    public void InitializeSpaceship()
    {      
        data = Global.Instance.PROFILE.spaceships.Find(x => x.model == model);
        if (data == null)
        {
            //Debug.LogWarning("<color=#FF0000>[SpaceshipController] \"data\" is null!</color>");
            //model = "ERROR";
            //return;

            data = new SpaceshipData()
            {
                model = "mk6",
                material = "Default",
                mk = 1,
            };
            Global.Instance.PROFILE.spaceships.Add(data);
        }

        config = Global.Instance.CONFIGS.spaceships.Find(x => x.model == model);
        if (config == null)
        {
            Debug.LogWarning("<color=#FF0000>[SpaceshipController] \"config\" is null!</color>");
            model = "ERROR";
            return;
        }

        meta = new SpaceshipMetadata(data, config);

        material = data.material;

        //LoadedMainMesh();
        LoadedMainMaterial();
    }

    public void SetTargetMovePoint(Transform _target)
    {
        StopAllCoroutines();

        ClearAllTargets();
        targetMovePoint = _target;
        StartCoroutine(ToMove(EnumDistanceType.ToPoint));
    }

    public void SetTargetFollow(Transform _target)
    {
        StopAllCoroutines();

        ClearAllTargets();
        targetFollow = _target;        
        StartCoroutine(ToFollow(EnumDistanceType.ToFollow));
    }

    public void SetTargetOrbit(Transform _target)
    {
        StopAllCoroutines();

        ClearAllTargets();
        targetOrbit = _target;
        StartCoroutine(ToOrbit(EnumDistanceType.ToFollow));
    }

    public void ClearAllTargets()
    {
        ClearTargetMovePoint();
        ClearTargetFollow();
        ClearTargetOrbit();
    }

    public void ClearTargetMovePoint()
    {
        targetMovePoint = null;
    }

    public void ClearTargetFollow()
    {
        targetFollow = null;
    }

    public void ClearTargetOrbit()
    {
        targetOrbit = null;
    }
    #endregion

    #region Private methods
    /*private void LoadedMainMesh()
    {
        Mesh mesh = Resources.Load(ConstantsResourcesPath.MODELS_SPACESHIPS + model, typeof(Mesh)) as Mesh;
        mainFilter.mesh = mesh;
    }*/

    private void LoadedMainMaterial()
    {
        Material mat = Resources.Load(ConstantsResourcesPath.MATERIALS_SPACESHIPS + model + "/" + material, typeof(Material)) as Material;
        mainRenderer.material = mat;
    }

    private float GetDistanceMinimum(EnumDistanceType _distanceType)
    {
        var result = 0.0f;
        switch (_distanceType)
        {
            case EnumDistanceType.ToPoint:
                //result = length / 0.5f;
                break;

            case EnumDistanceType.ToFollow:
                result = length * 3.0f;
                break;

            case EnumDistanceType.ToOrbit:
                result = length;
                break;

            case EnumDistanceType.ToObject:
                break;

            case EnumDistanceType.ToEnemy:
                break;
        }

        return result;
    }
    #endregion

    #region Coroutines
    //Перемещение к Цели
    private IEnumerator ToMove(EnumDistanceType _distanceType)
    {
        var distanceMin = GetDistanceMinimum(_distanceType);        
        var distance = Vector3.Distance(transform.position, targetMovePoint.position);

        //Пока есть Цель
        while (targetMovePoint)
        {
            //Поворачиваем в сторону Цели
            yield return ToRotate(targetMovePoint);

            //Движемся в сторону Цели
            if (distance <= distanceMin)
            {
                ClearTargetMovePoint();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetMovePoint.position, meta.Speed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, targetMovePoint.position);
            }
            
            yield return null;            
        }
    }

    //Следование за целью
    private IEnumerator ToFollow(EnumDistanceType _distanceType)
    {
        var distanceMin = GetDistanceMinimum(_distanceType);
        var distance = Vector3.Distance(transform.position, targetFollow.position);

        //Пока есть Цель
        while (targetFollow)
        {
            //Поворачиваем в сторону Цели
            yield return ToRotate(targetFollow);

            //Движемся в сторону Цели
            if (distance > distanceMin)
            {                
                transform.position = Vector3.MoveTowards(transform.position, targetFollow.position, meta.Speed * Time.deltaTime);                
            }

            distance = Vector3.Distance(transform.position, targetFollow.position);
            yield return null;
        }
    }

    //Вокруг орбиты
    private IEnumerator ToOrbit(EnumDistanceType _distanceType)
    {
        var distanceMin = GetDistanceMinimum(_distanceType);
        var distance = Vector3.Distance(transform.position, targetOrbit.position);

        //Пока есть Цель
        while (targetOrbit)
        {
            //Движемся к объекту
            if (distance > distanceMin * 1.15f)
            {
                targetMovePoint = targetOrbit;
                yield return ToMove(_distanceType);
            }
            //Двигаемся по орбите
            else
            {
                //Поворот
                var quaternionNeed = Quaternion.LookRotation(targetOrbit.position - transform.position) * Quaternion.AngleAxis(87.0f, Vector3.up);
                var step = meta.Maneuver * Time.deltaTime * 57.3f;  //В градусах
                transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternionNeed, step);

                //Движение
                transform.Translate(Vector3.forward * meta.Speed * Time.deltaTime);
            }

            distance = Vector3.Distance(transform.position, targetOrbit.position);
            yield return null;
        }
    }

    private IEnumerator ToRotate(Transform _target)
    {
        var targetRot = _target.position - transform.position;
        var quatToTarget = Quaternion.LookRotation(targetRot);
    
        //TODO: выловить БАГ в условии (бывает некорректное поведении при перемещении только по одной оси)
        while ((_target.position - transform.position) != Vector3.zero && quatToTarget != transform.rotation)
        {            
            var step = meta.Maneuver * Time.deltaTime;  //В радианах
            var newRot = Vector3.RotateTowards(transform.forward, targetRot, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newRot);

            yield return null;

            targetRot = _target.position - transform.position;
            quatToTarget = Quaternion.LookRotation(targetRot);
        }
    }
    #endregion
}
