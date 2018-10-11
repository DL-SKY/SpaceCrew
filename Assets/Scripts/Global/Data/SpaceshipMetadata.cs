using System;

public class SpaceshipMetadata
{
    #region Variables
    public int mk;

    private float armorMax;
    private float armor;
    public float Armor
    {
        get { return armor; }
        set { armor = value > armorMax ? armorMax : value; }
    }

    private float shieldMax;
    private float shield;
    public float Shield
    {
        get { return shield; }
        set { shield = value > shieldMax ? shieldMax : value; }
    }

    private float speedMax;
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value > speedMax ? speedMax : value; }
    }

    private float maneuverMax;
    private float maneuver;
    public float Maneuver
    {
        get { return maneuver; }
        set { maneuver = value > maneuverMax ? maneuverMax : value; }
    }

    private float energyMax;
    private float energy;
    public float Energy
    {
        get { return energy; }
        set { energy = value > energyMax ? energyMax : value; }
    }

    private float energyRecoveryMax;
    private float energyRecovery;
    public float EnergyRecovery
    {
        get { return energyRecovery; }
        set { energyRecovery = value > energyRecoveryMax ? energyRecoveryMax : value; }
    }
    #endregion

    #region Public methods
    public SpaceshipMetadata(SpaceshipData _data, SpaceshipsConfig _config)
    {
        mk = _data.mk;
        var mkIndex = mk - 1;
        if (mkIndex < 0)
            mkIndex = 0;

        speedMax = _config.speed[mkIndex];
        Speed = speedMax;

        maneuverMax = _config.maneuver[mkIndex];
        Maneuver = maneuverMax;

    }
    #endregion

    #region Private methods
    #endregion
}
