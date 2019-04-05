using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnergyShieldRendererController : MonoBehaviour
{
    #region Const
    private const string ALPHA_MOD = "_AlphaMod";
    private const string SPEED = "_Speed";
    private const string COLOR = "_Color";
    #endregion

    #region Variables
    public Color color;
    public Vector2 speedWaveAnimationRange = new Vector2(1.0f, 10.0f);
    public Vector2 alphaModRange = new Vector2(0.3f, 1.0f);

    [Header("VFX")]
    public ParticleSystem particlesDamage;

    private new MeshRenderer renderer;
    private Material material;
    #endregion

    #region Unity methods
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        material = renderer.material;
        material.SetColor(COLOR, color);

        UpdateMaterialSettings(1.0f);
    }
    #endregion

    #region Public methods
    public void UpdateMaterialSettings(float _shieldNormalize)
    {
        var alphaMod = Mathf.Lerp(alphaModRange.x, alphaModRange.y, _shieldNormalize);
        material.SetFloat(ALPHA_MOD, alphaMod);

        var speed = Mathf.Lerp(speedWaveAnimationRange.x, speedWaveAnimationRange.y, _shieldNormalize);
        material.SetFloat(SPEED, speed);
    }

    public void ShowDamageParticles(Vector3 _position, int _count = 30)
    {
        EmitParams eParams = new EmitParams();

        eParams.ResetPosition();        
        eParams.applyShapeToPosition = true;
 
        eParams.position = transform.InverseTransformPoint(_position);

        particlesDamage.Emit(eParams, _count);
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
