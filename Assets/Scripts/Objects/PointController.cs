using DllSky.Managers;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    #region Variables
    public bool autoInitializing = false;
    public EnumPointType type;
    public IDestructible destructible;

    [SerializeField]
    private bool visibleToCamera;
    public bool VisibleToCamera
    {
        get { return visibleToCamera; }
        set { visibleToCamera = value; }
    }

    private RendererController rendererController;
    #endregion

    #region Unity methods
    private void Awake()
    {
        rendererController = GetComponentInChildren<RendererController>();
    }

    private void Start()
    {
        if (autoInitializing)
            Initialize(type);
    }

    private void OnEnable()
    {
        if (rendererController)
            rendererController.OnVisibleToCamera += SetVisibleToCamera;        
    }

    private void OnDisable()
    {
        if (rendererController)
            rendererController.OnVisibleToCamera -= SetVisibleToCamera;
    }

    /*private void OnMouseUpAsButton()
    {
        //Debug.Log("OnMouseUpAsButton to Point");

        //OnClick();
    }*/
    #endregion

    #region Public methods
    public void Initialize(EnumPointType _type, IDestructible _destr = null)
    {
        type = _type;
        destructible = _destr;

        EventManager.CallOnInitPointController(this);
    }

    public void OnClick()
    {
        switch (type)
        {
            case EnumPointType.Point:
                EventManager.CallOnPoint(this, true);
                break;
            case EnumPointType.Enemy:
                EventManager.CallOnTargeting(this, true);
                break;
        }        
    }
    #endregion

    #region Private methods
    private void SetVisibleToCamera(bool _isVisible)
    {
        VisibleToCamera = _isVisible;
    }    
    #endregion

    #region Coroutines
    #endregion
}
