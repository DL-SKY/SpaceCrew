using DllSky.Components;
using DllSky.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogPanelShield : MarkerSubsystemDialogPanel
{
    #region Variables
    public ProgressBar shieldProgress;
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

        UpdateShieldPoints();
    }
    #endregion

    #region Private methods
    private void HandleOnUpdateHitPoints(PointController _controller)
    {
        if (_controller == pointController)
            UpdateShieldPoints();
    }

    private void UpdateShieldPoints()
    {
        shieldProgress.FillAmount = player.GetShieldNormalize();
    }
    #endregion

    #region Coroutines
    #endregion
}
