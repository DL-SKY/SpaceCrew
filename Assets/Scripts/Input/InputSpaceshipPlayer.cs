using DllSky.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSpaceshipPlayer : InputSpaceshipBase
{
    #region Variables
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        EventManager.eventOnPoint += HandlerOnPoint;
        EventManager.eventOnTargeting += HandlerOnTargeting;
    }

    private void OnDisable()
    {
        EventManager.eventOnPoint += HandlerOnPoint;
        EventManager.eventOnTargeting -= HandlerOnTargeting;
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    private void HandlerOnPoint(PointController _controller, bool _selected)
    {
        SetPoint(_controller, _selected);
    }

    private void HandlerOnTargeting(PointController _controller, bool _selected)
    {
        SetTarget(_controller, _selected);
    }
    #endregion

    #region Coroutines
    #endregion
}
