using DllSky.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMarker : MonoBehaviour
{
    #region Variables
    //public EnumUIMarkerType type;
    public Transform target;
    //public ProgressBar progressBar;
    public bool isInit = false;

    //[SerializeField]
    //private float distance;

    private RectTransform selfTransform;
    private PointController point;
    //private SpaceObject targetScript;
    //private Transform player;
    new private Camera camera;
    #endregion

    #region Unity methods
    private void Awake()
    {
        selfTransform = GetComponent<RectTransform>();
        camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (!isInit)
        {
            return;
        }
        if (!target || !point)
        {
            //DeleteMarker();
            return;
        }

        selfTransform.position = camera.WorldToScreenPoint(target.position);
        transform.localScale = (point.VisibleToCamera) ? Vector3.one : Vector3.zero;

        UpdateDistance();
    }
    #endregion

    #region Public methods
    public void Initialize(PointController _point)
    {
        //type = _type;

        //targetScript = _target;
        //target = targetScript.transform;
        //player = _player;

        point = _point;
        target = _point.transform;

        isInit = true;

        UpdateDistance();
    }

    public void UpdateDistance()
    {
        /*if (type == EnumUIMarkerType.Player)
        {
            return;
        }*/

        //distance = Vector3.Distance(player.position, target.position);
    }
    #endregion
}
