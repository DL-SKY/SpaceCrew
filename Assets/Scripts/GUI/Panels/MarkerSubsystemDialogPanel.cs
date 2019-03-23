using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSubsystemDialogPanel : MonoBehaviour
{
    #region Variables
    protected SpaceshipController player;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public virtual void Initialize()
    {
        player = PlayerController.Instance.player;
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
