using DllSky.Patterns;
using DllSky.Utility;
using System.Collections;
using UnityEngine;

public class GameSceneController : Singleton<GameSceneController>
{
    #region Variables
    [Header("Main")]
    public bool isInit = false;

    private CameraGame cameraGame;
    #endregion

    #region Unity methods
    private void Start()
    {
        //StartCoroutine(InitializeCoroutine());
    }

    private void OnEnable()
    {
        //Подписываемся на события
        //EventManager.eventOnStartPlayerTurn += HandlerOnStartPlayerTurn;
        //EventManager.eventOnEndPlayerTurn += HandlerOnEndPlayerTurn;
    }

    private void OnDisable()
    {
        //Отписываемся от событий
        //EventManager.eventOnStartPlayerTurn -= HandlerOnStartPlayerTurn;
        //EventManager.eventOnEndPlayerTurn -= HandlerOnEndPlayerTurn;
    }
    #endregion

    #region Public methods
    public void Initialize()
    {
        StartCoroutine(Initializing());
    }
    #endregion

    #region Private methods
    private void CameraPreparation()
    {
        cameraGame.Reposition();
    }   
    #endregion

    #region Coroutines
    private IEnumerator Initializing()
    {
        cameraGame = CameraGame.Instance;

        CameraPreparation();

        //Кадр для применения настроек создаваемых объектов на сцене
        yield return null;
        //------------------------------
        isInit = true;
    }
    #endregion
}
