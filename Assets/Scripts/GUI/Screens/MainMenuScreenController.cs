﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreenController : ScreenController
{
    #region Variables
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        if (IsInit)
            StartCoroutine(Initializing());//StartCoroutine(Show());
    }
    #endregion

    #region Buttons method
    public void OnClickSettings()
    {
        StartCoroutine(Dialog());
    }

    public void OnClickLogs()
    {
        ScreenManager.Instance.ShowDialog(ConstantsDialog.LOGS);
    }

    public void OnClickPlay()
    {
        StartCoroutine(StartPlay());
    }

    public void OnClickTest()
    {
        //MainGameManager.Instance.LoadScene("Test");
        StartCoroutine(StartTest());
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        base.Initialize(_data);

        StartCoroutine(Initializing());
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    private IEnumerator Initializing()
    {
        yield return MainGameManager.Instance.LoadSceneCoroutine(ConstantsScene.MAIN_MENU);

        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        yield return SplashScreenManager.Instance.HideSplashScreen();
        //yield return null;
    }

    private IEnumerator Dialog()
    {
        var dialog = ScreenManager.Instance.ShowDialog(ConstantsDialog.SETTINGS);
        yield return dialog.Wait();
    }

    private IEnumerator StartPlay()
    {
        //Прелоадер
        yield return SplashScreenManager.Instance.ShowBlack();
        //yield return new WaitForSeconds(2.5f);

        ScreenManager.Instance.ShowScreen(ConstantsScreen.GAME_SCREEN, Global.Instance.PROFILE.currentShip);
    }

    private IEnumerator StartTest()
    {
        //Прелоадер
        yield return SplashScreenManager.Instance.ShowBlack();

        ScreenManager.Instance.ShowScreen(ConstantsScreen.DEADSPACE);
    }
    #endregion
}
