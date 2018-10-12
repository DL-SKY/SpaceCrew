using DllSky.Extensions;
using DllSky.Managers;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadspaceScreenController : ScreenController
{
    #region Variales
    [Header("Markers")]
    public Transform markersPlace;
    private List<UIMarker> markers = new List<UIMarker>();
    //TODO: на время теста
    private List<PointController> points = new List<PointController>();


    #endregion

    #region Unity methods
    private void OnEnable()
    {
        //EventManager.eventOnClickEsc += OnClickEsc;

        if (IsInit)
            StartCoroutine(Show());
    }

    private void OnDisable()
    {
        //EventManager.eventOnClickEsc -= OnClickEsc;
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        StartCoroutine(Initializing());
    }
    #endregion

    #region Private methods
    private void CreateUIMarkers()
    {
        ClearAllMarkers();

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
            markerScr.Initialize(point);
        }
    }

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
        CreateUIMarkers();

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
