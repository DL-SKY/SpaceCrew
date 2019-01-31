using DllSky.Extensions;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    #region Variables
    public bool isPlayer = false;

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
    public EnumTargetType targetType;
    public Transform target;

    [Header("Main Renderer")]
    public MeshFilter mainFilter;
    public MeshRenderer mainRenderer;

    [Header("Camera")]
    public SpaceshipCameraPlace cameraPlace;

    private Rigidbody rb;
    private RendererController rendererController;

    [Header("Data")]
    [SerializeField]
    private SpaceshipData data;
    [Header("Configuration")]
    [SerializeField]
    private SpaceshipsConfig config;
    [Header("Metadata")]
    [SerializeField]
    private SpaceshipMetadata meta;

    //private Coroutine coroutine;
    private Coroutine speedCoroutine;
    #endregion

    #region Gizmo
    private void OnDrawGizmos()
    {
        //DrawGizmoSpaceship();
    }

    private void DrawGizmoSpaceship()
    {
        var file = isPlayer ? "SpaceshipSelf" : "SpaceshipEnemy";
        Gizmos.DrawIcon(transform.position, file);
    }
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

    private void FixedUpdate()
    {
        //Вращение
        FixedUpdateRotation();

        //Перемещение
        FixedUpdatePosition();        
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

            data = new SpaceshipData();
            Global.Instance.PROFILE.spaceships.Add(data);
        }

        config = data.GetConfig();

        meta = new SpaceshipMetadata(data, config);

        material = data.material;

        //LoadedMainMesh();
        LoadedMainMaterial();
    }

    public void SetTargetMovePoint(Transform _target)
    {
        targetType = EnumTargetType.ToPoint;

        target = _target;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public void SetSpeedNormalize(float _normalizeValue)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        speedCoroutine = StartCoroutine(meta.StartChangeSpeed(_normalizeValue));
    }

    public void AddSpeedNormalize(float _additionalNormalizeValue)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        var normalizeValue = meta.GetSpeedCurrentNormalize() + _additionalNormalizeValue;
        speedCoroutine = StartCoroutine(meta.StartChangeSpeed(normalizeValue));
    }

    public float GetSpeed()
    {
        return meta.GetParameter(EnumParameters.speed);
    }

    public float GetArmor()
    {
        return meta.GetParameter(EnumParameters.armor);
    }

    public float GetShield()
    {
        return meta.GetParameter(EnumParameters.shield);
    }
    #endregion

    #region Private methods
    private void SetVisibleToCamera(bool _isVisible)
    {
        VisibleToCamera = _isVisible;
    }

    private void FixedUpdateRotation()
    {
        if (target == null)
            return;

        //Вычисляем точку для поворота
        var targetRot = target.position - transform.position;
    
        if (targetRot != Vector3.zero && Vector3.Angle(transform.forward, targetRot) > 0.25)
        {
            var step = meta.GetParameter(EnumParameters.maneuver) * Time.fixedDeltaTime;  //В радианах (маневренность будем испоьзовать как скорость поворота в радианах)
            var newRot = Vector3.RotateTowards(transform.forward, targetRot, step, 0.0f);

            //transform.rotation = Quaternion.LookRotation(newRot);
            rb.rotation = Quaternion.LookRotation(newRot);
        }
    }

    private void FixedUpdatePosition()
    {
        //if (targetType != EnumTargetType.None && target == null)
        //return;

        if (target != null)
        {
            //TODO: добавить проверку на дистанцию. В случае необходимости останавливаем корабль
        }

        rb.MovePosition(transform.position + transform.forward * GetSpeed() * Time.fixedDeltaTime);
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

    private float GetDistanceMinimum(EnumTargetType _distanceType)
    {
        var result = 0.0f;
        switch (_distanceType)
        {
            case EnumTargetType.None:
                break;

            case EnumTargetType.ToPoint:
                //result = length / 0.5f;
                break;

            case EnumTargetType.ToFollow:
                result = length * 3.0f;
                break;

            case EnumTargetType.ToOrbit:
                result = length * 10.0f;
                break;

            case EnumTargetType.ToObject:
                break;

            case EnumTargetType.ToEnemy:
                break;
        }

        return result;
    }
    #endregion

    #region Coroutines    
    #endregion
}
