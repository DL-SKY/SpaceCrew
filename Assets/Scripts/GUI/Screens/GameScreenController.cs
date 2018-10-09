using DllSky.Managers;
using DllSky.Utility;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenController : ScreenController
{
    #region Variables
    [Header("Slots")]
    public Transform slotsParent;

    private GameSceneController sceneController;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        EventManager.eventOnClickEsc += OnClickEsc;

        LeanTouch.OnFingerTap += OnTap;
        LeanTouch.OnFingerSwipe += OnSwipe;

        if (IsInit)
            StartCoroutine(Show());
    }

    private void OnDisable()
    {
        EventManager.eventOnClickEsc -= OnClickEsc;

        LeanTouch.OnFingerTap -= OnTap;
        LeanTouch.OnFingerSwipe -= OnSwipe;
    }

    private void Update()
    {
        /*if (!IsInit)
            return;

        //Влево (на ПК)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerSpaceship.MoveToLeft();               //(true)
        }
        //Вправо (на ПК)
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerSpaceship.MoveToRight();              //(true)
        }

        //TEST: Missile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSpaceship.UseEquipment("testMissileLauncher");
        }*/
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        //var CONFIGS = Global.Instance.CONFIGS;
        //spaceshipID = _data as string;

        StartCoroutine(Initializing());
    }

    public void OnClickEsc()
    {
        if (!SplashScreenManager.Instance.IsOpenSplashscreen())
            StartCoroutine(CloseCoroutine());
    }
    #endregion

    #region Private methods   
    private void OnTap(LeanFinger _finger)
    {
        var tap = _finger.ScreenPosition;
        Debug.Log("<color=#FFD800>[INFO]</color> TAP: " + tap + " /OverGUI: " +  _finger.StartedOverGui.ToString());

        if (_finger.StartedOverGui)
            return;

        //var center = Screen.width / 2;
        /*
        //Тап влево
        if (tap.x < center)
            playerSpaceship.MoveToLeft();           //(true)
        //Тап вправо
        else if (tap.x > center)
            playerSpaceship.MoveToRight();          //(true)*/
    }

    private void OnSwipe(LeanFinger _finger)
    {
        var swipe = _finger.SwipeScreenDelta;
        Debug.Log("<color=#FFD800>[INFO]</color> Swipe: " + swipe + " /OverGUI: " + _finger.StartedOverGui.ToString());

        if (_finger.StartedOverGui)
            return;
        
        var absX = Mathf.Abs(swipe.x);
        var absY = Mathf.Abs(swipe.y);

        //Вертикальный свайп игнорируем
        if ( absY > absX)        
            return;
        /*
        //Свайп влево
        if (swipe.x < 0)
            playerSpaceship.MoveToLeft();           //(true)
        //Свайп вправо
        else if (swipe.x > 0)
            playerSpaceship.MoveToRight();          //(true)*/
    }    
    #endregion

    #region Coroutines
    private IEnumerator Initializing()
    {
        yield return MainGameManager.Instance.LoadSceneCoroutine(ConstantsScene.GAME_SCENE);

        sceneController = GameSceneController.Instance;
        sceneController.Initialize();

        while (!sceneController.isInit)
        {
            yield return null;
        }

        IsInit = true;

        StartCoroutine(Show());       
    }

    private IEnumerator Show()
    {
        yield return SplashScreenManager.Instance.HideSplashScreen();
    }

    private IEnumerator CloseCoroutine()
    {
        yield return SplashScreenManager.Instance.ShowBlack();

        Close();
    }
    #endregion
}
