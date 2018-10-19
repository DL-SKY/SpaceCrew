using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    #region Variables
    [Header("Base")]
    [SerializeField]
    private bool visibleToCamera;
    public bool VisibleToCamera
    {
        get { return visibleToCamera; }
        set { visibleToCamera = value; }
    }
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
    private RendererController rendererController;

    private SpaceshipData data;
    private SpaceshipsConfig config;
    [Header("Metadata")]
    [SerializeField]
    private SpaceshipMetadata meta;

    //private Coroutine coroutine;
    private Coroutine speedCoroutine;
    #endregion

    #region Unity methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rendererController = mainRenderer.GetComponent<RendererController>();
    }

    private void Start()
    {
        //TODO, пока тестирование
        InitializeSpaceship();
    }

    private void OnEnable()
    {
        if (rendererController)
            rendererController.OnVisibleToCamera += SetVisibleToCamera;
    }

    private void OnDisable()
    {
        if (rendererController)
            rendererController.OnVisibleToCamera -= SetVisibleToCamera;
    }
    #endregion

    #region Public methods
    public void InitializeSpaceship()
    {
        //coroutine = null;
        speedCoroutine = null;

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
        //if (coroutine != null)
            //StopCoroutine(coroutine);

        ClearAllTargets();
        targetMovePoint = _target;
        //coroutine = 
        StartCoroutine(ToMove(EnumDistanceType.ToPoint));
    }

    public void SetTargetFollow(Transform _target)
    {
        StopAllCoroutines();
        //if (coroutine != null)
            //StopCoroutine(coroutine);

        ClearAllTargets();
        targetFollow = _target;        
        //coroutine = 
        StartCoroutine(ToFollow(EnumDistanceType.ToFollow));
    }

    public void SetTargetOrbit(Transform _target)
    {
        StopAllCoroutines();
        //if (coroutine != null)
            //StopCoroutine(coroutine);

        ClearAllTargets();
        targetOrbit = _target;
        //coroutine = 
        StartCoroutine(ToOrbit(EnumDistanceType.ToOrbit));
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

    public void SetSpeed(float _normalizeValue)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        speedCoroutine = StartCoroutine(meta.StartChangeSpeed(_normalizeValue));
    }
    #endregion

    #region Private methods
    private void SetVisibleToCamera(bool _isVisible)
    {
        VisibleToCamera = _isVisible;
    }

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
                result = length * 10.0f;
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
                var quaternionNeed = Quaternion.LookRotation(targetOrbit.position - transform.position) * Quaternion.AngleAxis(89.0f, Vector3.up);
                var step = meta.Maneuver * Time.deltaTime * 57.3f;  //Радианы в градусы
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
        //Запоминаем текущую скорость
        var lastSpeedNormalize = meta.GetSpeedNormalize(meta.Speed);
        if (lastSpeedNormalize < ConstantsSpaceshipSettings.SPEED_DOCK)
            lastSpeedNormalize = ConstantsSpaceshipSettings.SPEED_DOCK;

        //Вычисляем точку для поворота
        var targetRot = _target.position - transform.position;
        //var quatToTarget = Quaternion.LookRotation(targetRot);        

        //TODO: выловить БАГ в условии (бывает некорректное поведении при перемещении только по одной оси)
        //while ((_target.position - transform.position) != Vector3.zero && quatToTarget != transform.rotation)
        while (targetRot != Vector3.zero && Vector3.Angle(transform.forward, targetRot) > 0.25)
        {
            //Уменьшаем скорость
            if (meta.GetSpeedNormalize(meta.Speed) > ConstantsSpaceshipSettings.SPEED_TURN)
            {
                if (speedCoroutine != null)
                    StopCoroutine(speedCoroutine);
                yield return speedCoroutine = StartCoroutine(meta.StartChangeSpeed(ConstantsSpaceshipSettings.SPEED_TURN));
            }

            var step = meta.Maneuver * Time.deltaTime;  //В радианах (маневренность будем испоьзовать как скорость поворота в радианах)
            var newRot = Vector3.RotateTowards(transform.forward, targetRot, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newRot);

            yield return null;

            targetRot = _target.position - transform.position;
            //quatToTarget = Quaternion.LookRotation(targetRot);
        }

        //Восстанавливаем предыдущую скорость
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);
        yield return speedCoroutine = StartCoroutine(meta.StartChangeSpeed(lastSpeedNormalize));
    }
    #endregion
}
