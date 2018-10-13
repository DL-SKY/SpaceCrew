using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    #region Variables
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

    private void OnMouseUpAsButton()
    {
        Debug.Log("OnMouseUpAsButton to Point");

        OnClick();
    }
    #endregion

    #region Public methods
    public void OnClick()
    {
        PlayerController.Instance.SetPoint(this);
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
