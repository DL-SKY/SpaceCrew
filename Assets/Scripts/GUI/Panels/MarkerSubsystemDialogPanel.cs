using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogPanel : MonoBehaviour
{
    #region Variables
    protected SpaceshipController player;
    protected PointController pointController;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public virtual void Initialize()
    {
        player = PlayerController.Instance.player;
        pointController = player.GetSelfPointController();
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
