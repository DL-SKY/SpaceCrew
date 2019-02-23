using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private ItemData data;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void Initialize(ItemData _data)
    {
        data = _data;
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
