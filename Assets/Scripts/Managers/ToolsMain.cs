using DllSky.Patterns;
using DllSky.Utility;
using System;
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
    #region Save
    [MenuItem("Tools/Save/Open profile folder")]
    private static void ToolsOpenProfileFolder()
    {
        //System.Diagnostics.Process.Start("explorer.exe", " " + Application.persistentDataPath);
        System.Diagnostics.Process.Start(Application.persistentDataPath);
        Debug.Log("<color=#FFD800>[ToolsMain]</color> " + Application.persistentDataPath);
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

    #region Email
    [MenuItem("Tools/Email")]
    private static void ToolsEmail()
    {
        LogManager.Instance.SendLogs();
    }
    #endregion

    #region Settings
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
    #endregion    

    #region Mesh
    [MenuItem("Tools/Mesh/Create convex mesh")]
    private static void CreateConvexMesh()
    {  
        var mesh = Selection.activeObject as Mesh;
        if (mesh == null)
        {
            EditorUtility.DisplayDialog("SELECT MESH", "You must select a Mesh first!", "OK");
            return;
        }

        var tStart = DateTime.Now;
        var full = AssetDatabase.GetAssetPath(Selection.activeObject);
        var path = full.Remove(full.LastIndexOf('/'));
        var name = mesh.name;

        List<Vector2> uv = new List<Vector2>();
        mesh.GetUVs(0, uv);

        var convexMesh = Utility.MeshUtility.CreateMesh(mesh);

        AssetDatabase.CreateAsset(convexMesh, string.Format(@"{0}/{1}_convex_collider.asset", path, name));

        Debug.Log("Convexing: " + (DateTime.Now - tStart).TotalSeconds);
    }
    #endregion
#endif
}

