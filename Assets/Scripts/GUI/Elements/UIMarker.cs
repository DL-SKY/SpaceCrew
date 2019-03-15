using DllSky.Components;
using DllSky.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMarker : MonoBehaviour
{
    #region Variables
    public bool isInit = false;
    public bool alwaysVisible = true;
    public float distance;

    [Space(5)]
    [SerializeField]
    private bool isActive;
    public bool IsActive
    {
        get { return isActive; }
        set {isActive = value; ApplyActive(); }
    }

    [Space(5)]
    public Transform targetTransform;
    private float screenCoef = 1.0f;

    [Header("ProgressBars")]
    public ProgressBar shieldProgress;
    public ProgressBar armorProgress;    

    [Header("Visible")]    
    public RectTransform selfTransformVisible;
    public RectTransform activeTarget;
    public RectTransform disableTarget;
    public Text distanceText;
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

    private void OnEnable()
    {
        EventManager.eventOnSetActiveTarget += HandlerOnSetActiveTarget;
        EventManager.eventOnUpdateHitPoints += HandleOnUpdateHitPoints;
    }

    private void OnDisable()
    {
        EventManager.eventOnSetActiveTarget -= HandlerOnSetActiveTarget;
        EventManager.eventOnUpdateHitPoints -= HandleOnUpdateHitPoints;
    }

    private void LateUpdate()
    {
        if (!isInit)
        {
            return;
        }
        if (!targetTransform || !pointController)
        {
            DeleteMarker();
            return;
        }
        
        if (alwaysVisible)          //Если Маркер отображается всегда
            UpdateAlwaysVisibleMarker();
        else                        //Иначе
            UpdateMarker(alwaysVisible);        
    }
    #endregion

    #region Public methods
    public void Initialize(PointController _point, RectTransform _parent, float _coef)
    {
        //Цель
        pointController = _point;
        targetTransform = _point.transform;

        //Родительский объект
        parent = _parent;

        //Коэффициент
        screenCoef = _coef;

        var sizeVisible = selfTransformVisible.sizeDelta.x / 2;
        halfVisibleSizeX = sizeVisible * screenCoef;
        halfVisibleSizeY = sizeVisible * screenCoef;

        var sizeInvisible = selfTransformInvisible.sizeDelta.x / 2;
        halfInvisibleSizeX = sizeInvisible * screenCoef;
        halfInvisibleSizeY = sizeInvisible * screenCoef;

        isInit = true;

        IsActive = false;

        UpdateHitPoints();
    }

    public void OnClickVisible()
    {
        if (pointController.type == EnumPointType.Player)
        {
            OnClickPlayer();
            return;
        }

        pointController.OnClick();
    }

    public void OnClickInvisible()
    {
        pointController.OnClick();
    }
    #endregion

    #region Private methods
    private void DeleteMarker()
    {
        Destroy(gameObject);
    }

    private void UpdateAlwaysVisibleMarker()
    {
        UpdateMarker(alwaysVisible);

        //Вычисляем положение
        var newPos = camera.WorldToScreenPoint(targetTransform.position);

        float halfSizeX, halfSizeY;

        halfSizeX = halfInvisibleSizeX;
        halfSizeY = halfInvisibleSizeY;

        var maxPosX = Screen.width - halfSizeX;
        var maxPosY = Screen.height - halfSizeY;

        newPos.x = Mathf.Clamp(newPos.x, halfSizeX, maxPosX);
        newPos.y = Mathf.Clamp(newPos.y, halfSizeY, maxPosY);

        //Маркер на объект за камерой прижимаем к бокам экрана
        //Если не верхняя/нижняя границы
        if (newPos.y > halfSizeY && newPos.y < maxPosY)
        {
            var targetRot = targetTransform.position - camera.transform.position;
            var angleForward = Vector3.Angle(camera.transform.forward, targetRot);

            if (angleForward < 90.0f)
            {
                newPos.x = newPos.x < ((maxPosX + halfSizeX) / 2) ? halfSizeX : maxPosX;
            }
            else
            {
                var angleRight = Vector3.Angle(camera.transform.right, targetRot);

                if (angleRight < 90.0f)
                    newPos.x = maxPosX;
                else
                    newPos.x = halfSizeX;
            }
        }

        //Новое расположение
        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }

    private void UpdateMarker(bool _alwaysVisible)
    {
        //Отображаемая часть Маркера
        selfTransformVisible.gameObject.SetActive(pointController.VisibleToCamera);

        if (_alwaysVisible)
            selfTransformInvisible.gameObject.SetActive(!pointController.VisibleToCamera);
        else
            selfTransformInvisible.gameObject.SetActive(false);

        //Проверка на необходимость дальнейших вычислений
        if (!pointController.VisibleToCamera)
            return;

        //Вычисляем положение
        var newPos = camera.WorldToScreenPoint(targetTransform.position);

        float halfSizeX, halfSizeY;

        halfSizeX = halfVisibleSizeX;
        halfSizeY = halfVisibleSizeY;

        newPos.x = Mathf.Clamp(newPos.x, halfSizeX, Screen.width - halfSizeX);
        newPos.y = Mathf.Clamp(newPos.y, halfSizeY, Screen.height - halfSizeY);        

        //Новое расположение
        transform.position = new Vector3(newPos.x, newPos.y, 0);

        //Дистанция до цели
        distance = (float)Math.Round(Vector3.Distance(pointController.transform.position, PlayerController.Instance.player.transform.position), 2);
        distanceText.text = distance.ToString();
    }

    private void HandlerOnSetActiveTarget(PointController _controller, bool _selected)
    {
        if (_controller == pointController)
            IsActive = _selected;
    }

    private void HandleOnUpdateHitPoints(PointController _controller)
    {
        if (_controller == pointController)
            UpdateHitPoints();
    }

    private void UpdateHitPoints()
    {
        if (shieldProgress)
        {
            shieldProgress.FillAmount = pointController.destructible.GetShieldNormalize();
        }

        if (armorProgress)
        {
            armorProgress.FillAmount = pointController.destructible.GetArmorNormalize();
        }        
    }

    private void ApplyActive()
    {
        if (activeTarget)
            activeTarget.gameObject.SetActive(IsActive);
        if (disableTarget)
            disableTarget.gameObject.SetActive(!IsActive);
    }

    private void OnClickPlayer()
    {
        IsActive = !IsActive;
    }
    #endregion
}
