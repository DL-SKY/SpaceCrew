using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerInfoDialogController : DialogController
{
    #region Variables
    private PointController pointController;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void Initialize(PointController _pointController, Action<bool> _callback)
    {
        pointController = _pointController;

        Callback = _callback;
    }
    #endregion

    #region Private methods
    #endregion
}
