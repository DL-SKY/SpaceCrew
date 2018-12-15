using DllSky.Patterns;
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
    [MenuItem("Tools/Save/Open profile folder")]
    private static void ToolsOpenProfileFolder()
    {
        //System.Diagnostics.Process.Start("explorer.exe", " " + Application.persistentDataPath);
        System.Diagnostics.Process.Start(Application.persistentDataPath);
        Debug.Log("<color=#FFD800>[ToolsMain]</color> " + Application.persistentDataPath);
    }

    [MenuItem("Tools/Email")]
    private static void ToolsEmail()
    {
        LogManager.Instance.SendLogs();
    }

    [MenuItem("Tools/Settings/Save settings")]
    private static void SaveSettings()
    {
        Global.Instance.SETTINGS.SaveSettings();
    }

    [MenuItem("Tools/Settings/Delete settings")]
    private static void DeleteSettings()
    {
        Global.Instance.SETTINGS.DeleteSettings();
    }

    [MenuItem("Tools/Save/Save profile")]
    private static void SaveProfile()
    {
        Global.Instance.PROFILE.SaveProfile();
    }

    [MenuItem("Tools/Save/Delete profile")]
    private static void DeleteProfile()
    {
        Global.Instance.PROFILE.DeleteProfile();
    }
    #endregion
#endif
}

