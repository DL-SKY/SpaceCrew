using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    #region Variables
    public string dialogName = "";
    public bool result = true;

    public Action<bool> Callback;

    protected bool isInit = false;

    private bool isOpened = true;    
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public virtual void Close(bool _result)
    {
        result = _result;
        isOpened = false;

        ScreenManager.Instance.CloseDialog(this);

        Callback?.Invoke(result);

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
