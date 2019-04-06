using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ArmorRendererController : MonoBehaviour
{
    #region Variables
    [Header("VFX")]
    public ParticleSystem particlesArmorDamage;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void ShowDamageParticles(Vector3 _position, int _count = 30)
    {
        EmitParams eParams = new EmitParams();

        eParams.ResetPosition();
        eParams.applyShapeToPosition = true;

        eParams.position = transform.InverseTransformPoint(_position);

        particlesArmorDamage.Emit(eParams, _count);
    }

    public void UpdateAdditionalFX(float _armorNormalize)
    {
        //TODO...
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
