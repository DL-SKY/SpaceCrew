using DllSky.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneController : Singleton<MainMenuSceneController>
{
    #region Variables
    //[Header("Settings")]
    //public bool isInit = false;
    #endregion

    #region Unity methods
    private void Start()
    {
        StartCoroutine(Initializing());
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    private IEnumerator Initializing()
    {
        yield return null;
        //-------------------------------
        //TODO: testing
        //------------------------------
        //ScreenManager.Instance.ShowScreen(ConstantsScreen.MAIN_MENU);
    }
    #endregion
}
