using DllSky.Patterns;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ЦВЕТА
//<color=#FFD800> </color> - желтый, информация
//<color=#FF0000> </color> - красный, ошибка, замечание

[System.Serializable]
public class LogManager : Singleton<LogManager>
{
    #region Variables
    public List<LogItem> logs = new List<LogItem>();
    #endregion

    #region Unity methods
    private void Start()
    {
        logs.Clear();
    }
    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    private void HandleLog(string _logString, string _stackTrace, LogType _type)
    {
        var newItem = new LogItem(_type, _logString);
        logs.Add(newItem);

        var newLog = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, "LogItem"), transform);
        newLog.transform.SetAsLastSibling();
        newLog.GetComponent<LogItemController>().Initialize(newItem);
    }
    #endregion

    #region Coroutines
    #endregion
}

[System.Serializable]
public class LogItem
{
    public LogType type;
    public string logString;

    public LogItem(LogType _type, string _logString)
    {
        type = _type;
        logString = _logString;
    }
}
