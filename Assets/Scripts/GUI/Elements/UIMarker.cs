﻿using DllSky.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMarker : MonoBehaviour
{
    #region Variables
    public bool isInit = false;
    public bool alwaysVisible = true;

    [Space(5)]
    public Transform targetTransform;
    private float screenCoef = 1.0f;

    [Header("Visible")]    
    public RectTransform selfTransformVisible;
    [SerializeField]
    private float halfVisibleSizeX;
    [SerializeField]
    private float halfVisibleSizeY;
    
    [Header("Invisible")]
    public RectTransform selfTransformInvisible;
    [SerializeField]
    private float halfInvisibleSizeX;
    [SerializeField]
    private float halfInvisibleSizeY;

    private RectTransform parent;
    private RectTransform selfTransform;
    private PointController pointController;
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
        if (!targetTransform || !pointController)
        {
            //DeleteMarker();
            return;
        }        

        //Отображаемая часть Маркера
        selfTransformVisible.gameObject.SetActive(pointController.VisibleToCamera);
        selfTransformInvisible.gameObject.SetActive(!pointController.VisibleToCamera);

        float halfSizeX, halfSizeY;
        if (pointController.VisibleToCamera)
        {
            halfSizeX = halfVisibleSizeX;
            halfSizeY = halfVisibleSizeY;
        }
        else
        {
            halfSizeX = halfInvisibleSizeX;
            halfSizeY = halfInvisibleSizeY;
        }

        //Вычисляем положение
        var newPos = camera.WorldToScreenPoint(targetTransform.position);

        newPos.x = Mathf.Clamp(newPos.x, halfSizeX, Screen.width - halfSizeX);
        newPos.y = Mathf.Clamp(newPos.y, halfSizeY, Screen.height - halfSizeY);

        //Новое расположение
        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }
    #endregion

    #region Public methods
    public void Initialize(PointController _point, RectTransform _parent, float _coef)
    {
        //Цель
        pointController = _point;
        targetTransform = _point.transform;

        parent = _parent;
        Debug.Log("Parent: " + parent.rect);

        //Коэффициент
        screenCoef = _coef;

        var sizeVisible = selfTransformVisible.sizeDelta.x / 2;
        halfVisibleSizeX = sizeVisible * screenCoef;
        halfVisibleSizeY = sizeVisible * screenCoef;
        var sizeInvisible = selfTransformInvisible.sizeDelta.x / 2;
        halfInvisibleSizeX = sizeInvisible * screenCoef;
        halfInvisibleSizeY = sizeInvisible * screenCoef;

        isInit = true;
    }

    public void OnClickVisible()
    {
        pointController.OnClick();
    }

    public void OnClickInvisible()
    {
        pointController.OnClick();
    }
    #endregion
}
