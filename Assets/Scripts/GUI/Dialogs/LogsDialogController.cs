using DllSky.Utility;
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
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    private void Show()
    {
        foreach (var log in logManager.logs)
        {
            var newLog = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, "LogItem"), scroller.content.transform);
            newLog.GetComponent<LogItemController>().Initialize(log, false);
        }
    }
    #endregion

    #region Coroutines
    #endregion
}
