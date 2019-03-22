using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerInfoDialogController : DialogController
{
    #region Variables
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void Initialize(Action<bool> _callback)
    {
        Callback = _callback;
    }
    #endregion

    #region Private methods
    #endregion
}
