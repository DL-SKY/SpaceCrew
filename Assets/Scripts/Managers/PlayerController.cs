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
        //player.transform.LookAt(_point.transform);
        player.SetTargetMove(_point.transform);
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
