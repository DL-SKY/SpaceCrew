using DllSky.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    #region Variables
    public SpaceshipController player;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void SetPoint(PointController _point)
    {
        player.SetTargetMovePoint(_point.transform);
        //player.SetTargetFollow(_point.transform);
        //player.SetTargetOrbit(_point.transform);
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
