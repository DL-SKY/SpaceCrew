using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldRendererController : MonoBehaviour
{
    #region Variables
    public Gradient ColorLifetime;

    private new MeshRenderer renderer;
    private Material material;
    #endregion

    #region Unity methods
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        material = renderer.material;
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
