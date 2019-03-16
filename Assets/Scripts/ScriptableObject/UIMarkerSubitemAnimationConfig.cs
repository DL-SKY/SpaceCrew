using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class UIMarkerSubitemAnimationConfig : ScriptableObject
{
    #region Variables
    [Header("Main animation")]
    public float time = 0.25f;    
    public AnimationCurve alpha = new AnimationCurve();
    public AnimationCurve size = new AnimationCurve();

    [Header("Text animation")]
    public float timeText = 0.25f;
    public AnimationCurve alphaText = new AnimationCurve();
    public AnimationCurve deltaTextPosition = new AnimationCurve();
    #endregion
}

﻿#if UNITY_EDITOR
public class UIMarkerSubitemAnimationConfigInspector : Editor
{
    [MenuItem("Tools/ScriptableObjects/UI/UIMarkerSubitemAnimationConfig")]
    public static void CreateDonutsCutsceneConfigAsset()
    {
        var asset = ScriptableObject.CreateInstance<UIMarkerSubitemAnimationConfig>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
            path = "Assets";
        else if (System.IO.Path.GetExtension(path) != "")
            path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New" + typeof(UIMarkerSubitemAnimationConfig).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
#endif
