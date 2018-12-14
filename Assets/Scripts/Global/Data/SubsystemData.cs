using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubsystemData
{
    #region Variables
    public string id;

    public List<ParameterData> parameters = new List<ParameterData>();
    public List<string> skills = new List<string>();
    #endregion
}
