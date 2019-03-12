﻿using DllSky.Extensions;
using DllSky.Managers;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipController : MonoBehaviour, IDestructible
{
    #region Variables
    public bool isPlayer = false;

    [Header("Input Controller")]
    public InputSpaceshipBase input;

    [Header("Base")]
    [SerializeField]
    private bool visibleToCamera;
    public bool VisibleToCamera
    {
        get { return visibleToCamera; }
        set { visibleToCamera = value; }
    }
    public string model;
    public EnumSizeType SizeType { get; set; }
    public string material;
    public float length;

    [Header("Targets")]
    private PointController selfPointController;
    public EnumSpeedType maxSpeedType;
    public EnumTargetType pointType;
    public Transform point;
    public List<PointController> targets;

    [Header("Main Renderer")]
    public MeshFilter mainFilter;
    public MeshRenderer mainRenderer;
    public MainEnginesRendererController mainEnginesRenderer;
    public Transform weaponsParent;
    public List<Transform> weaponSlots = new List<Transform>();

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

        //TODO, test
        if (isPlayer)
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

    private void Update()
    {
        //Обновление визуальных эффектов
        //Следы двигателя
        UpdateMainEnginesRenderer();
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
        //ShowShieldDamage(other.ClosestPoint(transform.position));
        ShowShieldDamage(shieldCollider.ClosestPoint(other.transform.position));
    }
    #endregion

    #region Public methods
    public void InitializeSpaceship()
    {
        //Input
        ApplyInpytComponent();

        //coroutine = null;
        speedCoroutine = null;
        point = null;
        targets.Clear();

        data = Global.Instance.PROFILE.spaceships.Find(x => x.model == model);
        if (data == null)
        {
            //Debug.LogWarning("<color=#FF0000>[SpaceshipController] \"data\" is null!</color>");
            //model = "ERROR";
            //return;

            data = new SpaceshipData();

            //TODO: TEST
            data.ApplyDefault();

            Global.Instance.PROFILE.spaceships.Add(data);
        }

        config = data.GetConfig();

        meta = new SpaceshipMetadata(data, config);

        SizeType = (EnumSizeType)config.sizeType;

        ApplyWeapons();

        material = data.material;
        //LoadedMainMesh();
        LoadedMainMaterial();

        CreateMarker();
    }

    public void ClearPoint()
    {
        point = null;
    }

    public void SetSpeedNormalize(float _normalizeValue)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        _normalizeValue = _normalizeValue > GetMaxSpeedForCurrentSpeedType() ? GetMaxSpeedForCurrentSpeedType() : _normalizeValue;
        speedCoroutine = StartCoroutine(meta.StartChangeSpeed(_normalizeValue));

        brakingDistance = GetDistanceBraking(minDistance, _normalizeValue);
    }

    public void AddSpeedNormalize(float _additionalNormalizeValue)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        var normalizeValue = meta.GetSpeedCurrentNormalize() + _additionalNormalizeValue;
        normalizeValue = normalizeValue > GetMaxSpeedForCurrentSpeedType() ? GetMaxSpeedForCurrentSpeedType() : normalizeValue;
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

    public void SetPoint(PointController _controller, bool _selected)
    {
        if (_selected)
        {
            if (point)
                EventManager.CallOnSetActiveTarget(point.GetComponent<PointController>(), false);

            pointType = EnumTargetType.ToPoint;
            minDistance = GetDistanceMinimum(pointType);
            brakingDistance = GetDistanceBraking(minDistance, meta.GetSpeedResultNormalize());
            point = _controller.transform;

            EventManager.CallOnSetActiveTarget(_controller, true);
        }
        else
        {
            pointType = EnumTargetType.None;
            point = null;

            EventManager.CallOnSetActiveTarget(_controller, false);
        }
    }

    public void SetTarget(PointController _controller, bool _selected)
    {
        if (_selected && targets.Count < meta.GetParameter(EnumParameters.targets))
        {
            if (!targets.Contains(_controller))
            {
                targets.Add(_controller);
                EventManager.CallOnSetActiveTarget(_controller, true);
            }
        }
        else if (!_selected)
        {
            targets.Remove(_controller);
            EventManager.CallOnSetActiveTarget(_controller, false);
        }
    }

    public void ApplyDamage(Damage _damage, Vector3 _weaponPos)
    {
        var shieldDmg = CalculateDamageShield(_damage);
        var armorDmg = CalculateDamageArmor(_damage);

        if (shieldDmg != 0.0f)
        {
            meta.SetDeltaParameter(EnumParameters.shield, shieldDmg);

            if (shieldDmg > 0.0f)
                ShowShieldDamage(shieldCollider.ClosestPoint(_weaponPos));
        }

        //TODO: Условие получения урона брони
        //Варианты: малый заряд щитов или его отсутствие; атака противника игнорирует щиты и т.д.
        var needCheckDamageArmor = GetShield() <= 0.0f;

        if (needCheckDamageArmor && armorDmg != 0.0f)
            meta.SetDeltaParameter(EnumParameters.armor, armorDmg);

        EventManager.CallOnUpdateHitPoints(selfPointController);

        CheckDestruction();
    }    

    public float GetShieldNormalize()
    {
        return meta.GetCurrentParameterNormalize(EnumParameters.shield);
    }

    public float GetArmorNormalize()
    {
        return meta.GetCurrentParameterNormalize(EnumParameters.armor);
    }

    public void SetMaxSpeedType(EnumSpeedType _type)
    {
        maxSpeedType = _type;
        SetSpeedNormalize(GetMaxSpeedForCurrentSpeedType());
    }
    #endregion

    #region Private methods
    private void ApplyInpytComponent()
    {
        var _input = GetComponent<InputSpaceshipBase>();

        if (_input == null)
        {
            if (isPlayer)
                _input = gameObject.AddComponent<InputSpaceshipPlayer>();
            else
                _input = gameObject.AddComponent<InputSpaceshipBase>();
        }

        input = _input;
        input.Initialize(this);
    }

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
        if (point == null)
            return;

        //Вычисляем точку для поворота
        var targetRot = point.position - transform.position;

        if (targetRot != Vector3.zero && Vector3.Angle(transform.forward, targetRot) > 0.25)
        {
            var step = meta.GetParameter(EnumParameters.maneuver) * ConstantsGameSettings.MANEUVER_MOD_ROTATE * Time.fixedDeltaTime;
            var newRot = Vector3.RotateTowards(transform.forward, targetRot, step, 0.0f);

            rb.rotation = Quaternion.LookRotation(newRot);
        }
    }

    private void FixedUpdatePosition()
    {
        //if (targetType == EnumTargetType.None)
        //return;

        var speed = GetSpeed();

        if (point != null)
        {
            //Проверка на дистанцию. В случае необходимости останавливаем корабль
            var distance = Vector3.Distance(transform.position, point.position);

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
                    SetSpeedNormalize(GetMaxSpeedForCurrentSpeedType());
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
        var brakingTime = resultSpeed / (meta.GetParameter(EnumParameters.maneuver) * ConstantsGameSettings.MANEUVER_MOD_SPEED);
        var result = _min + ((resultSpeed * brakingTime) / 2.0f);             //Производная формулы свободного падения, в данном случае подходит

        return result;
    }

    private void ApplyWeapons()
    {
        //TODO
        //Сделать инстанцирование вооружения в соответствующие слоты

        //Создаем на Корабле установленное в нем вооружение
        foreach (var weaponData in meta.weapons)
        {
            var newWeapon = new GameObject(weaponData.id, typeof(WeaponController)).GetComponent<WeaponController>();
            newWeapon.transform.SetParent(weaponsParent);
            newWeapon.Initialize(weaponData, this);
        }
    }

    private float CalculateDamageShield(Damage _damage)
    {
        var result = -_damage.ShieldDmg;

        var shield = GetShield();

        //TODO:

        return result;
    }

    private float CalculateDamageArmor(Damage _damage)
    {
        var result = -_damage.ArmorDmg;

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

    private void ShowShieldDamage(Vector3 _position)
    {
        shieldController.ShowDamageParticles(_position);
    }

    private void UpdateMainEnginesRenderer()
    {
        (mainEnginesRenderer as IUpdateRenderer).UpdateRenderer(meta);
    }

    private void CreateMarker()
    {
        if (isPlayer)
            return;

        var pointController = GetComponent<PointController>();
        if (!pointController)
            pointController = gameObject.AddComponent<PointController>();

        pointController.Initialize(EnumPointType.Enemy, this);

        selfPointController = pointController;
    }

    private float GetMaxSpeedForCurrentSpeedType()
    {
        var result = 0.0f;

        switch (maxSpeedType)
        {
            case EnumSpeedType.Stop:
                result = ConstantsSpaceshipSettings.SPEED_STOP;
                break;
            case EnumSpeedType.Dock:
                result = ConstantsSpaceshipSettings.SPEED_DOCK;
                break;
            case EnumSpeedType.Cruising:
                result = ConstantsSpaceshipSettings.SPEED_CRUISING;
                break;
            case EnumSpeedType.Full:
                result = ConstantsSpaceshipSettings.SPEED_FULL;
                break;
        }

        return result;
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
        //SetTargetMovePoint(point);
    }

    [ContextMenu("Show Shield Damage")]
    private void MenuShowShieldDamage()
    {
        //ShowShieldDamage(transform.position + new Vector3(0.0f, 0.0f, 0.5f));
        ShowShieldDamage(transform.InverseTransformPoint(transform.position + new Vector3(0.0f, 0.0f, 0.5f)));
        ShowShieldDamage(transform.InverseTransformPoint(transform.position + new Vector3(0.0f, 0.0f, -0.5f)));
        ShowShieldDamage(transform.InverseTransformPoint(transform.position + new Vector3(0.0f, 0.5f, 0.0f)));
    }

    [ContextMenu("Apply Damage")]
    private void MenuApplyDamage()
    {
        var dmg = new Damage(0.5f, 0.5f, 0.0f);
        ApplyDamage(dmg, Vector3.zero);
    }
    #endregion
}
