﻿using System;
using System.Collections.Generic;

[Serializable]
public class ItemData
{
    #region Variables
    public string id;

    public List<ParameterData> SelfParameters = new List<ParameterData>();
    public List<ParameterData> ShipParameters = new List<ParameterData>();
    public List<string> skills = new List<string>();
    #endregion
}