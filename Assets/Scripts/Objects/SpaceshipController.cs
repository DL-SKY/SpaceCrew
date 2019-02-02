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

    [Header("Energy shield Renderer")]
    public EnergyShieldRendererController shieldController;

    [Header("Colliders")]
    public Collider mainCollider;
    public Collider shieldCollider;

    [Header("Camera Point")]
    public Transform cameraPoint;

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
    private float minDistance;              //Расстояние до цели, на котором корабль останавливается
    private float brakingDistance;          //Расстояние, на котором корабль начинает снижать скорость
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

        SetCameraTarget();
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

    #region Collisions
    private void OnCollisionEnter(Collision collision)
    {
        //test
        Debug.Log("OnCollisionEnter " + collision.gameObject.name);
        Debug.Log("OnCollisionEnter " + collision.collider.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        //test
        Debug.Log("OnTriggerEnter " + other.gameObject.name);
        Debug.Log("OnTriggerEnter " + other.tag);
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
        minDistance = GetDistanceMinimum(targetType);
        brakingDistance = GetDistanceBraking(minDistance, meta.GetSpeedResultNormalize());
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

        brakingDistance = GetDistanceBraking(minDistance, _normalizeValue);
    }

    public void AddSpeedNormalize(float _additionalNormalizeValue)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        var normalizeValue = meta.GetSpeedCurrentNormalize() + _additionalNormalizeValue;
        speedCoroutine = StartCoroutine(meta.StartChangeSpeed(normalizeValue));

        brakingDistance = GetDistanceBraking(minDistance, normalizeValue);
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

    public void ApplyDamage(Damage _damage)
    {
        var shieldDmg = CalculateDamageShield(_damage);
        var armorDmg = CalculateDamageArmor(_damage);

        if (shieldDmg != 0.0f)
            meta.SetDeltaParameter(EnumParameters.shield, shieldDmg);

        //TODO: Условие получения урона брони
        //Варианты: малый заряд щитов или его отсутствие; атака противника игнорирует щиты и т.д.
        var needCheckDamageArmor = GetShield() > 0.0f;

        if (needCheckDamageArmor && armorDmg != 0.0f)
            meta.SetDeltaParameter(EnumParameters.armor, armorDmg);

        CheckDestruction();
    }
    #endregion

    #region Private methods
    private void SetCameraTarget()
    {
        var cameraController = Camera.main.GetComponentInParent<SpaceshipCameraController>();

        if (cameraController)
            cameraController.SetTarget(cameraPoint);
    }

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
            var step = meta.GetParameter(EnumParameters.maneuver) * ConstantsGameSettings.MANEUVER_MOD_ROTATE * Time.fixedDeltaTime;
            var newRot = Vector3.RotateTowards(transform.forward, targetRot, step, 0.0f);

            rb.rotation = Quaternion.LookRotation(newRot);
        }
    }

    private void FixedUpdatePosition()
    {
        if (targetType == EnumTargetType.None)
            return;

        var speed = GetSpeed();

        if (target != null)
        {
            //TODO: добавить проверку на дистанцию. В случае необходимости останавливаем корабль
            var distance = Vector3.Distance(transform.position, target.position);

            //Если не достигли конечной точки
            if (distance > minDistance)
            {
                if (speed > 0.0f)
                {
                    //Пора включать торможение
                    if (distance <= brakingDistance && meta.GetSpeedResultNormalize() > 0.0f)
                        SetSpeedNormalize(0.0f);
                }
                else
                {
                    //Добавляем скорость
                    SetSpeedNormalize(ConstantsSpaceshipSettings.SPEED_COMMON);
                }
            }
        }

        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
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
                result = length / 0.5f;
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

    private float GetDistanceBraking(float _min, float _speedNormalize)
    {
        var resultSpeed = meta.GetSpeedValue(_speedNormalize);
        var brakingTime = resultSpeed / ( meta.GetParameter(EnumParameters.maneuver) * ConstantsGameSettings.MANEUVER_MOD_SPEED);
        var result = _min + ( (resultSpeed * brakingTime) / 2.0f );             //Производная формулы свободного падения, в данном случае подходит

        return result;
    }

    private float CalculateDamageShield(Damage _damage)
    {
        var result = 0.0f;

        var shield = GetShield();
        
        //TODO:

        return result;
    }

    private float CalculateDamageArmor(Damage _damage)
    {
        var result = 0.0f;

        var armor = GetArmor();

        //TODO:

        return result;
    }

    private void CheckDestruction()
    {
        var armor = GetArmor();

        if (armor <= 0.0f)
        {
            //TODO:
        }
    }
    #endregion

    #region Coroutines    
    #endregion

    #region Menu
    [ContextMenu("Set Camera Target")]
    private void MenuSetCameraTarget()
    {
        SetCameraTarget();
    }

    [ContextMenu("Set Target")]
    private void MenuSetTarget()
    {
        SetTargetMovePoint(target);
    }
    #endregion
}
