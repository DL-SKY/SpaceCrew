using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogController : DialogController
{
    #region Variables
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void Initialize(EnumSubsystems _sbs, Action<bool> _callback)
    {
        Callback = _callback;
    }
    #endregion

    #region Private methods
    #endregion
}
