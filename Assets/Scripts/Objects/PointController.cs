using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    #region Variables
    #endregion

    #region Unity methods
    private void OnMouseUpAsButton()
    {
        Debug.Log("OnClick to Point");

        PlayerController.Instance.SetPoint(this);
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
