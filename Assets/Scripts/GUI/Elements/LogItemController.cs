using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogItemController : MonoBehaviour
{
    #region Variables
    public Text text;
    public Text time;
    public float lifeTime = 1.5f;
    #endregion

    #region Public methods
    public void Initialize(LogItem _log, bool _selfDestroy = true)
    {
        text.text = _log.logString;
        time.text = _log.timeString;

        if (_selfDestroy)
            StartCoroutine(Timer());
    }
    #endregion

    #region Coroutines
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    #endregion
}
