using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    #region Variables
    public bool result = true;
    private bool isOpened = true;
    private bool isInit = false;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public virtual void Close(bool _result)
    {
        result = _result;
        isOpened = false;

        ScreenManager.Instance.CloseDialog(this);

        Destroy(gameObject);
    }

    public void InitSplashScreen()
    {
        isInit = true;
    }

    public void CloseSplashScreen()
    {
        GetComponent<Animator>().Play("Hide");
    }

    public void CloseSplashScreenImmediately()
    {
        result = true;
        isOpened = false;       

        Destroy(gameObject);
    }
    #endregion

    #region Protected methods
    #endregion

    #region Coroutine
    public IEnumerator WaitShowSplashScreen()
    {
        while (!isInit)
            yield return null;
    }

    public IEnumerator Wait()
    {
        while (isOpened)
            yield return null;
    }
    #endregion
}
