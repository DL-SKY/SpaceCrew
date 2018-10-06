using DllSky.Utility;
using UnityEngine;
using UnityEngine.UI;

public class LogsDialogController : DialogController
{
    #region Variables
    public ScrollRect scroller;

    private LogManager logManager;
    #endregion

    #region Unity methods
    private void Awake()
    {
        logManager = LogManager.Instance;
        
        Show();
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
    public void SendLogs()
    {
        LogManager.Instance.SendLogs();
    }
    #endregion

    #region Private methods
    private void Show()
    {
        foreach (var log in logManager.logs)
        {
            var newLog = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, "LogItem"), scroller.content.transform);
            newLog.GetComponent<LogItemController>().Initialize(log, false);
        }

        //scroller.
    }

    private void HandleLog(string _logString, string _stackTrace, LogType _type)
    {
        var newItem = new LogItem(_type, _logString);
        
        var newLog = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, "LogItem"), scroller.content.transform);
        newLog.GetComponent<LogItemController>().Initialize(newItem, false);
    }
    #endregion

    #region Coroutines
    #endregion
}
