using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpaceshipMetadata
{
    #region Variables
    [Header("Subsystems")]
    public List<SubsystemData> subsystems = new List<SubsystemData>();          //Список подсистем (каждая подсистема содержит перки/скиллы)
    [Header("Weapons")]
    public List<ItemData> weapons = new List<ItemData>();                       //Список установленного вооружения
    [Header("Slots")]
    public List<ItemData> slots = new List<ItemData>();                         //Список установленного оборудования
    [Header("Items")]
    public List<ItemData> items = new List<ItemData>();                         //Список используемых предметов

    [Header("Mark")]
    public int mk;                                                              //Модель, модификация

    [Header("Max parameters")]
    public Dictionary<EnumParameters, float> parametersMax = new Dictionary<EnumParameters, float>();

    [Header("Current parameters")]
    public Dictionary<EnumParameters, float> parameters = new Dictionary<EnumParameters, float>();




    [Header("Main subsystems")]
    [SerializeField]
    private float armorMax;
    [SerializeField]
    private float armor;
    public float Armor
    {
        get { return armor; }
        set { armor = value > armorMax ? armorMax : value; }
    }

    [SerializeField]
    private float cargoMax;
    [SerializeField]
    private float cargo;
    public float Cargo
    {
        get { return cargo; }
        set { cargo = value > cargoMax ? cargoMax : value; }
    }

    [Header("Shield subsystems")]
    [SerializeField]
    private float shieldMax;
    [SerializeField]
    private float shield;
    public float Shield
    {
        get { return shield; }
        set { shield = value > shieldMax ? shieldMax : value; }
    }

    [SerializeField]
    private float shieldRecoveryMax;
    [SerializeField]
    private float shieldRecovery;
    public float ShieldRecovery
    {
        get { return shieldRecovery; }
        set { shieldRecovery = value > shieldRecoveryMax ? shieldRecoveryMax : value; }
    }

    [Header("Navigation subsystems")]
    [SerializeField]
    private float speedMax;
    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value > speedMax ? speedMax : value; }
    }

    [SerializeField]
    private float maneuverMax;
    [SerializeField]
    private float maneuver;
    public float Maneuver
    {
        get { return maneuver; }
        set { maneuver = value > maneuverMax ? maneuverMax : value; }
    }

    [Header("Weapons subsystems")]
    [SerializeField]
    private float accuracyMax;
    [SerializeField]
    private float accuracy;
    public float Accuracy
    {
        get { return accuracy; }
        set { accuracy = value > accuracyMax ? accuracyMax : value; }
    }

    [SerializeField]
    private float criticalMax;
    [SerializeField]
    private float critical;
    public float Critical
    {
        get { return critical; }
        set { critical = value > criticalMax ? criticalMax : value; }
    }

    [Header("Energy subsystems")]
    [SerializeField]
    private float energyMax;
    [SerializeField]
    private float energy;
    public float Energy
    {
        get { return energy; }
        set { energy = value > energyMax ? energyMax : value; }
    }

    [SerializeField]
    private float energyRecoveryMax;
    [SerializeField]
    private float energyRecovery;
    public float EnergyRecovery
    {
        get { return energyRecovery; }
        set { energyRecovery = value > energyRecoveryMax ? energyRecoveryMax : value; }
    }

    private int mkIndex;
    private SpaceshipData data;
    private SpaceshipsConfig config;
    #endregion

    #region Public methods
    public SpaceshipMetadata(SpaceshipData _data, SpaceshipsConfig _config)
    {
        data = _data;
        config = _config;

        mk = data.mk;
        mkIndex = data.GetMkIndex();

        ApplySubsystems();

        ApplyMaxParameters();
        ApplyParameters();


        //Main subsystems
        armorMax = GetArmorMax();
        Armor = armorMax;
        cargoMax = GetCargoMax();
        Cargo = cargoMax;

        //Shield subsystems
        shieldMax = GetShieldMax();
        Shield = shieldMax;
        shieldRecoveryMax = GetShieldRecoveryMax();
        ShieldRecovery = shieldRecoveryMax;

        //Navigation subsystems
        speedMax = GetSpeedMax();
        Speed = speedMax;
        maneuverMax = GetManeuverMax();
        Maneuver = maneuverMax;

        //Weapons subsystems
        accuracyMax = GetAccuracyMax();
        Accuracy = accuracyMax;
        criticalMax = GetCriticalMax();
        Critical = criticalMax;

        //Energy subsystems
        energyMax = GetEnergyMax();
        Energy = energyMax;
        energyRecoveryMax = GetEnergyRecoveryMax();
        EnergyRecovery = energyRecoveryMax;
       
    }

    public float GetSpeedNormalize(float _speed)
    {
        return _speed / speedMax;
    }

    public float GetSpeedValue(float _normalizeValue)
    {
        _normalizeValue = Mathf.Clamp01(_normalizeValue);

        return speedMax * _normalizeValue;
    }
    #endregion

    #region Private methods
    private float GetArmorMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.armor[mkIndex];

        return result;
    }

    private float GetCargoMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.cargo[mkIndex];

        return result;
    }

    private float GetShieldMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.shield[mkIndex];

        return result;
    }

    private float GetShieldRecoveryMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.shieldRecovery[mkIndex];

        return result;
    }

    private float GetSpeedMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.speed[mkIndex];

        return result;
    }

    private float GetManeuverMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.maneuver[mkIndex];

        return result;
    }

    private float GetAccuracyMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.accuracy[mkIndex];

        return result;
    }

    private float GetCriticalMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.critical[mkIndex];

        return result;
    }

    private float GetEnergyMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.energy[mkIndex];

        return result;
    }

    private float GetEnergyRecoveryMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.energyRecovery[mkIndex];

        return result;
    }

    private void ApplySubsystems()
    {
        subsystems.Clear();

        var configs = Global.Instance.CONFIGS.subsystems;
        foreach (var item in configs)
        {
            var newSubsystem = new SubsystemData((EnumSubsystems)Enum.Parse(typeof(EnumSubsystems), item.id), config, mkIndex);
            subsystems.Add(newSubsystem);
        }
    }

    private void ApplyMaxParameters()
    {
        parametersMax.Clear();

        foreach (var item in subsystems)
        {
            foreach (var param in item.parameters)
            {
                parametersMax.Add(param.id, param.value);
            }
        }

        Debug.LogWarning("parametersMax: " + parametersMax.Count);
    }

    private void ApplyParameters()
    {
        parameters.Clear();

        foreach (var param in parametersMax)
        {
            parameters.Add(param.Key, param.Value);
        }

        Debug.LogWarning("parameters: " + parameters.Count);
    }
    #endregion

    #region Coroutines
    public IEnumerator StartChangeSpeed(float _normalizeValue)
    {     
        var speedResult = GetSpeedValue(_normalizeValue);
        if (speedResult == Speed)
            yield break;
        var modifier = Speed > speedResult ? -1.0f : 1.0f;

        Debug.LogWarning("START StartChangeSpeed: " + Speed + "/" + speedResult);

        while (true)
        {
            Speed += modifier * Maneuver * Time.deltaTime;

            //Проверка
            if (modifier < 0)
            {
                if (Speed < speedResult)
                {
                    Speed = speedResult;
                    break;
                }
            }
            else
            {
                if (Speed > speedResult)
                {
                    Speed = speedResult;
                    break;
                }
            }

            yield return null;
        }

        Debug.LogWarning("STOP StartChangeSpeed: " + Speed + "/" + speedResult);
    }
    #endregion
}
