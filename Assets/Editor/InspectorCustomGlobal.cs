using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Global))]
public class InspectorCustomGlobal : Editor
{
    #region Unity methods
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //EditorGUI.BeginChangeCheck();
        //EditorGUI.EndChangeCheck();

        GUILayout.Space(10);
        if (GUILayout.Button("Проверить файл конфигурации"))
            OnClickCheckConfig();

        GUILayout.Space(5);
        if (GUILayout.Button("Сохранить настройки"))
            OnClickSaveSettings();
        if (GUILayout.Button("Удалить настройки"))
            OnClickDeleteSettings();

        GUILayout.Space(5);
        if (GUILayout.Button("Сохранить профиль"))
            OnClickSaveProfile();
        if (GUILayout.Button("Удалить профиль"))
            OnClickDeleteProfile();
    }
    #endregion

    #region Private methods
    private void OnClickCheckConfig()
    {
        ((Global)target).CheckConfig();
    }

    private void OnClickSaveSettings()
    {
        ((Global)target).SaveSettings();
    }

    private void OnClickDeleteSettings()
    {
        ((Global)target).DeleteSettings();
    }

    private void OnClickSaveProfile()
    {
        ((Global)target).SaveProfile();
    }

    private void OnClickDeleteProfile()
    {
        ((Global)target).DeleteProfile();
    }
    #endregion
}
