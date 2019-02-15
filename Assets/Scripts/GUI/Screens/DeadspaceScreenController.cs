using DllSky.Extensions;
using DllSky.Managers;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadspaceScreenController : ScreenController
{
    #region Variales
    [Header("Markers")]
    public Transform markersPlace;
    private List<UIMarker> markers = new List<UIMarker>();
    private RectTransform markersParent;
    private float screenCoef;
    //TODO: на время теста
    //private List<PointController> points = new List<PointController>();


    #endregion

    #region Unity methods
    private void Awake()
    {
        markersParent = markersPlace.GetComponent<RectTransform>();
        var scaler = ScreenManager.Instance.GetComponent<CanvasScaler>();
        screenCoef = Screen.height / scaler.referenceResolution.y;
    }

    private void OnEnable()
    {
        //EventManager.eventOnClickEsc += OnClickEsc;
        EventManager.eventOnInitPointController += OnInitPointController;

        if (IsInit)
            StartCoroutine(Show());
    }

    private void OnDisable()
    {
        //EventManager.eventOnClickEsc -= OnClickEsc;
        EventManager.eventOnInitPointController -= OnInitPointController;
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        StartCoroutine(Initializing());
    }
    #endregion

    #region Private methods
    private void OnInitPointController(PointController _controller)
    {
        var markerObj = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, ConstantsPrefabName.MARKER_POINT), markersPlace);
        var markerScr = markerObj.GetComponent<UIMarker>();
        markers.Add(markerScr);
        markerScr.Initialize(_controller, markersParent, screenCoef);
    }

    /*private void CreateUIMarkers()
    {
        ClearAllMarkers();

        var parent = markersPlace.GetComponent<RectTransform>();

        var scaler = ScreenManager.Instance.GetComponent<CanvasScaler>();
        var screenCoef = Screen.height / scaler.referenceResolution.y;

        //Игрок
        //var markerPlayerObj = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, "MarkerObject"), markersPlace);
        //var markerPlayerScr = markerPlayerObj.GetComponent<UIMarker>();
        //markers.Add(markerPlayerScr);
        //markerPlayerScr.Initialize(sceneController.GetPlayer(), player.transform, EnumUIMarkerType.Player);

        //Объекты
        points.AddRange(FindObjectsOfType<PointController>());
        foreach (var point in points)
        {
            var markerObj = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, "MarkerPoint"), markersPlace);
            var markerScr = markerObj.GetComponent<UIMarker>();
            markers.Add(markerScr);
            markerScr.Initialize(point, parent, screenCoef);
        }
    }*/

    private void ClearAllMarkers()
    {
        markers.Clear();
        markersPlace.DestroyChildren();
    }
    #endregion

    #region Coroutines
    private IEnumerator Initializing()
    {
        yield return MainGameManager.Instance.LoadSceneCoroutine("Test");

        /*sceneController = GameSceneController.Instance;
        sceneController.Initialize();

        while (!sceneController.isInit)
        {
            yield return null;
        }*/

        yield return new WaitForSeconds(2.5f);
        //CreateUIMarkers();

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
