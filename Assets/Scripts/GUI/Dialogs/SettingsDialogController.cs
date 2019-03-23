using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsDialogController : DialogController
{
    #region Variables
    [Header("Test")]
    public Text acceleration;
    public Text gyro;
    #endregion

    #region Unity methods
    private void Update()
    {
        acceleration.text = string.Format("{0} : {1} : {2}", Input.acceleration.x, Input.acceleration.y , Input.acceleration.z);
        gyro.text = string.Format("{0}", Input.gyro.attitude);
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
