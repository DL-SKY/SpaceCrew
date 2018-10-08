using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    #region Variables
    public string id;

    public Dictionary<string, float> parameters = new Dictionary<string, float>();
    public List<string> skills = new List<string>();
    #endregion
}
