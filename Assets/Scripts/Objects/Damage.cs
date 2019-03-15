public class Damage
{
    #region Variables
    public float ArmorDmg { get; }
    public float ShieldDmg { get; }
    public float Critical { get; }
    #endregion

    #region Constructors
    public Damage(float _armorDmg, float _shieldDmg, float _critical)
    {
        ArmorDmg = _armorDmg;
        ShieldDmg = _shieldDmg;
        Critical = _critical;
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion
}
