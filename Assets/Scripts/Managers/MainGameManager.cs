using DllSky.Managers;
using DllSky.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : Singleton<MainGameManager>
{
    #region Variables
    private string currentScene = null;
    #endregion

    #region Unity methods
    private void Start()
    {
        StartCoroutine(StartGame());
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

        Debug.Log("[MyGameManager] Application.targetFrameRate: " + Application.targetFrameRate);
    }
    #endregion

    #region Coroutine
    private IEnumerator StartGame()
    {
        //Стартовый прелоадер
        yield return SplashScreenManager.Instance.ShowStartingGame();
        //Версия
        Debug.Log("[VERSION] " + Application.version);
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
            yield return SceneManager.UnloadSceneAsync(oldScene);
        }

        //Загружаем новую сцену
        currentScene = _scene;
        yield return SceneManager.LoadSceneAsync(currentScene, _mode);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
    }
    #endregion
}
