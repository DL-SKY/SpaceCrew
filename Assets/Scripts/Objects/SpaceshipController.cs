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

        targetMovePoint = _target;
        StartCoroutine(ToMove(EnumDistanceType.ToPoint));
    }

    public void ClearTargetMovePoint()
    {
        targetMovePoint = null;
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

            case EnumDistanceType.ToObject:
                break;

            case EnumDistanceType.ToEnemy:
                break;
        }

        return result;
    }
    #endregion

    #region Coroutines
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

    private IEnumerator ToRotate(Transform _target)
    {
        var targetRot = _target.position - transform.position;
        var quatToTarget = Quaternion.LookRotation(targetRot);
    
        while ((_target.position - transform.position) != Vector3.zero && quatToTarget != transform.rotation)
        {
            
            var step = meta.Maneuver * Time.deltaTime;
            var newRot = Vector3.RotateTowards(transform.forward, targetRot, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newRot);

            yield return null;

            targetRot = _target.position - transform.position;
            quatToTarget = Quaternion.LookRotation(targetRot);
        }
    }
    #endregion
}
