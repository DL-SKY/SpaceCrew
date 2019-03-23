using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogPanelNavigation : MarkerSubsystemDialogPanel
{
    #region Variables
    #endregion

    #region Unity methods
    private void Start()
    {
        Initialize();
    }
    #endregion

    #region Public methods
    public override void Initialize()
    {
        base.Initialize();
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
    #endregion

    #region Coroutines
    #endregion
}
