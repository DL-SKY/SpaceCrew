using DllSky.Components;
using DllSky.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogPanelMain : MarkerSubsystemDialogPanel
{
    #region Variables
    public ProgressBar armorProgress;

    
    #endregion

    #region Unity methods
    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        EventManager.eventOnUpdateHitPoints += HandleOnUpdateHitPoints;
    }

    private void OnDisable()
    {
        EventManager.eventOnUpdateHitPoints -= HandleOnUpdateHitPoints;
    }
    #endregion

    #region Public methods
    public override void Initialize()
    {
        base.Initialize();

        UpdateArmorPoints();
    }
    #endregion

    #region Private methods
    private void HandleOnUpdateHitPoints(PointController _controller)
    {
        if (_controller == pointController)
            UpdateArmorPoints();
    }

    private void UpdateArmorPoints()
    {
        armorProgress.FillAmount = player.GetArmorNormalize();
    }
    #endregion

    #region Coroutines
    #endregion
}
