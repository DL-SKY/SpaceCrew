using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubsystemData
{
    #region Variables
    public EnumSubsystems id;

    public List<ParameterData> parameters = new List<ParameterData>();
    public List<string> skills = new List<string>();

    private int mkIndex;
    #endregion

    #region Public methods
    public SubsystemData(EnumSubsystems _id, SpaceshipsConfig _config, int _mkIndex)
    {
        id = _id;
        mkIndex = _mkIndex;

        parameters.Clear();
        skills.Clear();

        //Наполняем по конфигу перечнем параметров
        var paramsTMP = Global.Instance.CONFIGS.parameters.FindAll(x => x.subsystem == id.ToString());
        foreach (var paramTMP in paramsTMP)
        {
            var value = GetParamValue(paramTMP.id, _config);
            var newParam = new ParameterData((EnumParameters)Enum.Parse(typeof(EnumParameters), paramTMP.id), value);
            parameters.Add(newParam);
        }
    }
    #endregion

    #region Private methods
    private float GetParamValue(string _id, SpaceshipsConfig _config)
    {
        var result = 0.0f;
        var param = (EnumParameters)Enum.Parse(typeof(EnumParameters), _id);

        switch (param)
        {
            case EnumParameters.armor:
                result = _config.armor[mkIndex];
                break;

            case EnumParameters.cargo:
                result = _config.cargo[mkIndex];
                break;

            case EnumParameters.shield:
                result = _config.shield[mkIndex];
                break;

            case EnumParameters.shieldRecovery:
                result = _config.shieldRecovery[mkIndex];
                break;

            case EnumParameters.speed:
                result = _config.speed[mkIndex];
                break;

            case EnumParameters.maneuver:
                result = _config.maneuver[mkIndex];
                break;

            case EnumParameters.accuracy:
                result = _config.accuracy[mkIndex];
                break;

            case EnumParameters.critical:
                result = _config.critical[mkIndex];
                break;

            case EnumParameters.energy:
                result = _config.energy[mkIndex];
                break;

            case EnumParameters.energyRecovery:
                result = _config.energyRecovery[mkIndex];
                break;
        }

        return result;
    }
    #endregion
}
