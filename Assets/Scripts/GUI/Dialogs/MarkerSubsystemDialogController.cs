using DllSky.Extensions;
using DllSky.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogController : DialogController
{
    #region Variables
    [Header("Links")]
    public Transform middlePanel;

    private MarkerSubsystemDialogPanel panel;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void Initialize(EnumSubsystems _sbs, Action<bool> _callback)
    {
        Callback = _callback;

        middlePanel.DestroyChildren();

        string name = "";
        switch (_sbs)
        {
            case EnumSubsystems.main:
                name = ConstantsPrefabName.MARKER_SUBSYSTEM_MAIN;
                break;

            case EnumSubsystems.shield:
                name = ConstantsPrefabName.MARKER_SUBSYSTEM_SHIELD;
                break;

            case EnumSubsystems.navigation:
                name = ConstantsPrefabName.MARKER_SUBSYSTEM_NAVIGATION;
                break;

            case EnumSubsystems.weapon:
                name = ConstantsPrefabName.MARKER_SUBSYSTEM_WEAPON;
                break;

            case EnumSubsystems.energy:
                name = ConstantsPrefabName.MARKER_SUBSYSTEM_ENERGY;
                break;
        }

        GameObject _panel = ResourcesManager.LoadPrefab(ConstantsResourcesPath.PANELS, name);
        panel = Instantiate(_panel, middlePanel).GetComponent<MarkerSubsystemDialogPanel>();
    }
    #endregion

    #region Private methods
    #endregion
}
