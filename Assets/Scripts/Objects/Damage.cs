public class Damage
{
    #region Variables
    public float Value { get; }
    public float ModShield { get; }
    public float ModArmor { get; }
    #endregion

    #region Constructors
    public Damage(float _damage, float _modShield, float _modArmor)
    {
        Value = _damage;
        ModShield = _modShield;
        ModArmor = _modArmor;
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion
}
