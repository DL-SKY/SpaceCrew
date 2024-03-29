﻿using DllSky.Managers;
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

    //[Header("Max parameters")]
    private Dictionary<EnumParameters, float> parametersMax = new Dictionary<EnumParameters, float>();
    //[Header("Current parameters")]
    private Dictionary<EnumParameters, float> parameters = new Dictionary<EnumParameters, float>();    

    private int mkIndex;
    private SpaceshipData data;
    private SpaceshipsConfig config;
    private bool isPlayer;

    private float speedResultNormalize;                                         //Нормализованная скорость, к которой стремимся
    #endregion

    #region Public methods
    public SpaceshipMetadata(SpaceshipData _data, SpaceshipsConfig _config, bool _isPlayer)
    {
        data = _data;
        config = _config;
        isPlayer = _isPlayer;

        mk = data.mk;
        mkIndex = data.GetMkIndex();

        ApplySubsystems();
        ApplyWeapons();
        ApplySlots();
        ApplyItems();

        ApplyMaxParameters();
        ApplyParameters();

        ApplyDefault();
    }

    public int GetConfigWeapons()
    {
        return config.weapons[mkIndex];
    }

    public float GetMaxParameter(EnumParameters _key)
    {
        if (parametersMax.ContainsKey(_key))
            return parametersMax[_key];
        else
            return 0.0f;
    }

    public float GetParameter(EnumParameters _key)
    {
        if (parameters.ContainsKey(_key))
            return parameters[_key];
        else
            return 0.0f;
    }

    public void SetDeltaParameter(EnumParameters _key, float _value)
    {
        var max = GetMaxParameter(_key);

        parameters.AddOrUpdate(_key, _value);

        if (GetParameter(_key) > max)
            parameters[_key] = max;

        if (_key == EnumParameters.speed && isPlayer)
            EventManager.CallOnPlayerChangeSpeed();
    }

    public void SetParameter(EnumParameters _key, float _value)
    {
        var max = GetMaxParameter(_key);

        if (!parameters.ContainsKey(_key))
            parameters.Add(_key, _value);
        else
            parameters[_key] = _value;

        if (GetParameter(_key) > max)
            parameters[_key] = max;

        if (_key == EnumParameters.speed && isPlayer)
            EventManager.CallOnPlayerChangeSpeed();
    }

    public float GetSpeedNormalize(float _speed)
    {
        return Mathf.InverseLerp(0.0f, GetMaxParameter(EnumParameters.speed), _speed);
    }    

    public float GetSpeedValue(float _normalizeValue)
    {
        _normalizeValue = Mathf.Clamp01(_normalizeValue);

        return GetMaxParameter(EnumParameters.speed) * _normalizeValue;
    }

    public float GetSpeedCurrentNormalize()
    {
        return GetSpeedNormalize(GetParameter(EnumParameters.speed));
    }

    public float GetCurrentParameterNormalize(EnumParameters _param)
    {
        return Mathf.InverseLerp(0.0f, GetMaxParameter(_param), GetParameter(_param));
    }

    public float GetSpeedResultNormalize()
    {
        return speedResultNormalize;
    }

    public float GetAverageWeaponOptimalDistance()
    {
        var sumDistance = 0.0f;

        for (int i = 0; i < weapons.Count; i++)
            sumDistance += weapons[i].GetSelfParameter(EnumParameters.optimalDistance);

        return sumDistance / weapons.Count;
    }
    #endregion

    #region Private methods
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

    private void ApplyWeapons()
    {
        weapons.Clear();

        var configs = Global.Instance.CONFIGS.items;
        foreach (var weapon in data.weapons)
        {
            var config = configs.Find(x => x.id == weapon);
            var newWeapon = new ItemData(config);

            weapons.Add(newWeapon);
        }
    }

    private void ApplySlots()
    {
        slots.Clear();


    }

    private void ApplyItems()
    {
        items.Clear();


    }

    private void ApplyMaxParameters()
    {
        parametersMax.Clear();

        foreach (var item in subsystems)
        {
            foreach (var param in item.parameters)
            {
                parametersMax.AddOrUpdate(param.id, param.value);
            }
        }
    }

    private void ApplyParameters()
    {
        parameters.Clear();

        foreach (var param in parametersMax)
        {
            parameters.AddOrUpdate(param.Key, param.Value);
        }
    }

    private void ApplyDefault()
    {
        SetParameter(EnumParameters.speed, 0.0f);
    }
    #endregion

    #region Coroutines
    public IEnumerator StartChangeSpeed(float _normalizeValue)
    {
        speedResultNormalize = _normalizeValue;

        var speedResult = GetSpeedValue(_normalizeValue);
        if (speedResult == GetParameter(EnumParameters.speed))
            yield break;
        var modifier = GetParameter(EnumParameters.speed) > speedResult ? -1.0f : 1.0f;

        Debug.LogWarning("START StartChangeSpeed: " + GetParameter(EnumParameters.speed) + "/" + speedResult);

        while (true)
        {
            var delta = modifier * GetParameter(EnumParameters.maneuver) * ConstantsGameSettings.MANEUVER_MOD_SPEED * Time.deltaTime;
            SetDeltaParameter(EnumParameters.speed, delta);

            //Проверка
            if (modifier < 0)
            {
                if (GetParameter(EnumParameters.speed) < speedResult)
                {
                    SetParameter(EnumParameters.speed, speedResult);
                    break;
                }
            }
            else
            {
                if (GetParameter(EnumParameters.speed) > speedResult)
                {
                    SetParameter(EnumParameters.speed, speedResult);
                    break;
                }
            }

            yield return null;
        }

        Debug.LogWarning("STOP StartChangeSpeed: " + GetParameter(EnumParameters.speed) + "/" + speedResult);
    }
    #endregion
}
