using DllSky.Components;
using DllSky.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogPanelNavigation : MarkerSubsystemDialogPanel
{
    #region Variables
    public ProgressBar speedProgress;
    #endregion

    #region Unity methods
    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        EventManager.eventOnPlayerChangeSpeed += HandlerOnPlayerChangeSpeed;
    }

    private void OnDisable()
    {
        EventManager.eventOnPlayerChangeSpeed -= HandlerOnPlayerChangeSpeed;
    }
    #endregion

    #region Public methods
    public override void Initialize()
    {
        base.Initialize();

        UpdateSpeed();
    }

    public void OnClickSpeedStop()
    {
        player.SetMaxSpeedType(EnumSpeedType.Stop);
    }

    public void OnClickSpeedDock()
    {
        player.SetMaxSpeedType(EnumSpeedType.Dock);
    }

    public void OnClickSpeedCruising()
    {
        player.SetMaxSpeedType(EnumSpeedType.Cruising);
    }

    public void OnClickSpeedFull()
    {
        player.SetMaxSpeedType(EnumSpeedType.Full);
    }
    #endregion

    #region Private methods
    private void HandlerOnPlayerChangeSpeed()
    {
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        speedProgress.FillAmount = player.GetSpeedNormalize();
    }
    #endregion

    #region Coroutines
    #endregion
}
