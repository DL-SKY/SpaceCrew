﻿using DllSky.Patterns;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ToolsMain : Singleton<ToolsMain>
{
    #region Variables
    public bool defaultFlag = false;
    #endregion

    #region Unity methods    
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion

#if UNITY_EDITOR
    #region Menu
    [MenuItem("Tools/Open profile folder")]
    private static void OpenProfileFolder()
    {
        //System.Diagnostics.Process.Start("explorer.exe", " " + Application.persistentDataPath);
        System.Diagnostics.Process.Start(Application.persistentDataPath);
        Debug.Log("<color=#FFD800>[ToolsMain]</color> " + Application.persistentDataPath);
    }

    [MenuItem("Tools/Email")]
    private static void Email()
    {
        string body = JsonUtility.ToJson((LogManager)LogManager.Instance, true);
        string url = string.Format("mailto:{0}?subject={1}&body={2}", "alex.dllsky@gmail.com", WWW.EscapeURL("subject"), WWW.EscapeURL(body));
        Application.OpenURL(url);
    }
    #endregion
#endif
}

