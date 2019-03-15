using DllSky.Analytics;
using DllSky.Managers;
using DllSky.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : Singleton<MainGameManager>
{
    #region Variables
    private string currentScene = null;

    private DateTime startSession;
    #endregion    

    #region Unity methods
    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private void OnApplicationQuit()
    {
        //Метрики
        var session = DateTime.UtcNow - startSession;
        var gameOverData = new AnaliticsGameOverData(string.Format("{0} min.", (int)session.TotalMinutes));
        AnalyticsManager.Instance.SendEvent(EnumAnalyticsEventType.GameOver, gameOverData);
    }
    #endregion

    #region Public methods
    public void LoadScene(string _scene, LoadSceneMode _mode = LoadSceneMode.Additive)
    {
        StartCoroutine( LoadSceneCoroutine(_scene, _mode) );
    }
    #endregion

    #region Private methods
    private void ApplySettings()
    {
        Application.targetFrameRate = 60;
        QualitySettings.antiAliasing = 4;

        Debug.Log("<color=#FFD800>[MainGameManager] Application.targetFrameRate: " + Application.targetFrameRate + "</color>");
    }
    #endregion

    #region Coroutine
    private IEnumerator StartGame()
    {
        //Стартовый прелоадер
        yield return SplashScreenManager.Instance.ShowStartingGame();

        //Метрики
        startSession = DateTime.UtcNow;
        AnalyticsManager.Instance.SendEvent(EnumAnalyticsEventType.GameStart, null);

        //Версия
        Debug.Log("<color=#FFD800>[VERSION] " + Application.version + "</color>");
        //Инициализация конфига
        Global.Instance.Initialize();
        //Ожидание окончания загрузки конфига и настроек
        while (!Global.Instance.isComplete)
            yield return null;

        ApplySettings();

        //TEST 1: Загрузка тестовой сцены
        yield return new WaitForSeconds(1.0f);



        //yield return SceneManager.LoadSceneAsync(ConstantsScene.MAIN_MENU, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(ConstantsScene.MAIN_MENU));

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //LoadScene(ConstantsScene.MAIN_MENU);
        ScreenManager.Instance.ShowScreen(ConstantsScreen.MAIN_MENU);

        //while (!MainMenuSceneController.Instance.isInit)
        //yield return null;

        //yield return SplashScreenManager.Instance.HideSplashScreenImmediately();
        //-------------------

        //yield return SplashScreenManager.Instance.ShowBlack();


        //TEST 2:
        //ScreenManager.Instance.ShowScreen(ConstantsScreen.MAIN_MENU);
        //-------------------

        //Test3:
        //int amount = 50000;
        //Debug.Log(DllSky.Utility.UtilityBase.GetStringFormatAmount3(amount));
        //Debug.Log(DllSky.Utility.UtilityBase.GetStringFormatAmount5(amount));
        //Debug.Log(DllSky.Utility.UtilityBase.GetStringFormatAmount6(amount));
        //-------------------

        //TEST 3:
        //yield return new WaitForSeconds(2.5f);
        //LoadScene(ConstantsScene.CAREER);


    }

    public IEnumerator LoadSceneCoroutine(string _scene, LoadSceneMode _mode = LoadSceneMode.Additive)
    {
        //Выгружаем предыдущую сцену
        if (SceneManager.sceneCount > 1)
        {
            var oldScene = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
            var oldName = oldScene.name;
            yield return SceneManager.UnloadSceneAsync(oldScene);
            Debug.Log("<color=#FFD800>[MainGameManager] Scene unloaded: " + oldName + "</color>");
        }

        Resources.UnloadUnusedAssets();
        GC.Collect();

        //Загружаем новую сцену
        currentScene = _scene;
        yield return SceneManager.LoadSceneAsync(currentScene, _mode);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
        Debug.Log("<color=#FFD800>[MainGameManager] Scene loaded: " + currentScene + "</color>");
    }
    #endregion
}
