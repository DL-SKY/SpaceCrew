using DllSky.Managers;
using DllSky.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{
    #region Constants
    private float RADAR_SCALER = 1.5f;
    private float RADIUS = 128.0f;
    #endregion

    #region Variables
    public Transform direction;
    public Transform markersParent;
    public Transform playerMarker;

    private Camera cam;
    private List<RadarMarker> markers = new List<RadarMarker>();
    private Transform player;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        EventManager.eventOnInitPointController += OnInitPointController;
    }

    private void OnDisable()
    {
        EventManager.eventOnInitPointController += OnInitPointController;
    }

    private void LateUpdate()
    {
        if (cam == null)
            cam = Camera.main;
        if (cam == null)
            return;

        if (player == null)
            player = PlayerController.Instance.player?.transform;
        if (player == null)
            return;

        //Вращаем сам Радар
        RotateRadar();

        //Вращаем стрелку направления
        RotateDirection();

        //Обновляем позиции маркеров
        UpdateMarkers();
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    private void OnInitPointController(PointController _controller)
    {
        string name = "";

        switch (_controller.type)
        {
            case EnumPointType.Point:
                name = ConstantsPrefabName.RADAR_POINT;
                break;

            case EnumPointType.Enemy:
                name = ConstantsPrefabName.RADAR_SPACESHIP;
                break;
        }

        var markerObj = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, name), markersParent);
        markers.Add(new RadarMarker() { marker = markerObj.transform, controller = _controller });
    }

    private void RotateRadar()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, cam.transform.eulerAngles.y);
    }

    private void RotateDirection()
    {
        direction.localEulerAngles  = new Vector3(0.0f, 0.0f, -player.eulerAngles.y);
    }

    private void UpdateMarkers()
    {
        playerMarker.localEulerAngles = -transform.localEulerAngles;

        for (int i = 0; i < markers.Count; i++)
        {
            var marker = markers[i];

            if (marker.controller == null)
            {
                Destroy(marker.marker.gameObject);
                markers.Remove(marker);

                continue;
            }

            var targetPos = marker.controller.transform.position;
            
            //Чистые значения позиции
            var x = (targetPos.x - player.position.x) * RADAR_SCALER;
            var y = (targetPos.z - player.position.z) * RADAR_SCALER;
            
            //Округленные (не выходящие за окружность)
            if (Vector2.Distance(Vector2.zero, new Vector2(x, y)) > RADIUS)
            {
                var a = Vector2.Distance(new Vector2(x, y), new Vector2(RADIUS, 0.0f));
                var b = Vector2.Distance(new Vector2(x, y), Vector2.zero);
                var c = Vector2.Distance(Vector2.zero, new Vector2(RADIUS, 0.0f));

                var cosA = (b * b + c * c - a * a) / (2 * b * c);
                var mod = y > 0 ? 1.0f : -1.0f;
                var A = Mathf.Acos(cosA); //В радианах

                x = RADIUS * Mathf.Cos(A * mod);
                y = RADIUS * Mathf.Sin(A * mod);
            }        

            marker.marker.localPosition = new Vector3(x, y, 0.0f);
            marker.marker.localEulerAngles = -transform.localEulerAngles;
        }
    }
    #endregion
}

public struct RadarMarker
{
    public Transform marker;
    public PointController controller;
}
