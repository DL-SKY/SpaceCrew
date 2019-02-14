using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    #region Variables
    public string id;

    public List<ParameterData> selfParameters = new List<ParameterData>();
    public List<ParameterData> shipParameters = new List<ParameterData>();
    public List<string> skills = new List<string>();
    #endregion

    #region Public methods
    public ItemData(ItemsConfig _config)
    {
        id = _config.id;

        ApplySelfParameters(_config.selfParameters);
        ApplyShipParameters(_config.shipParameters);
        ApplySkills(_config.skills);
    }
    #endregion

    #region Private methods
    private void ApplySelfParameters(string[] parameters)
    {
        selfParameters.Clear();

        foreach (var param in parameters)
        {
            var split = param.Split(':');
            var enumParams = (EnumParameters)Enum.Parse(typeof(EnumParameters), split[0]);
            var val = float.Parse(split[1], System.Globalization.CultureInfo.InvariantCulture);     //Парсинг строки с точкой

            selfParameters.Add(new ParameterData(enumParams, val));
        }
    }

    private void ApplyShipParameters(string[] parameters)
    {
        shipParameters.Clear();

        foreach (var param in parameters)
        {
            var split = param.Split(':');
            var enumParams = (EnumParameters)Enum.Parse(typeof(EnumParameters), split[0]);
            var val = float.Parse(split[1], System.Globalization.CultureInfo.InvariantCulture);     //Парсинг строки с точкой

            shipParameters.Add(new ParameterData(enumParams, val));
        }
    }

    private void ApplySkills(string[] parameters)
    {
        skills.Clear();

        foreach (var param in parameters)
        {
            skills.Add(param);
        }
    }
    #endregion
}
