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

    private int mkIndex;
    private SpaceshipData data;
    private SpaceshipsConfig config;
    #endregion

    #region Public methods
    public SpaceshipMetadata(SpaceshipData _data, SpaceshipsConfig _config)
    {
        data = _data;
        config = _config;
        mk = data.mk;
        mkIndex = mk - 1;
        if (mkIndex < 0)
            mkIndex = 0;

        armorMax = GetArmorMax();
        Armor = armorMax;

        shieldMax = GetShieldMax();
        Shield = shieldMax;

        speedMax = GetSpeedMax();
        Speed = speedMax;

        maneuverMax = GetManeuverMax();
        Maneuver = maneuverMax;

        //energyMax = GetEnergyMax();
        //Energy = energyMax;

        //energyRecoveryMax = GetEnergyRecoveryMax();
        //EnergyRecovery = energyRecoveryMax;
    }
    #endregion

    #region Private methods
    private float GetArmorMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.armor[mkIndex];

        return result;
    }

    private float GetShieldMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.shield[mkIndex];

        return result;
    }

    private float GetSpeedMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.speed[mkIndex];

        return result;
    }

    private float GetManeuverMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.maneuver[mkIndex];

        return result;
    }

    private float GetEnergyMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.energy[mkIndex];

        return result;
    }

    private float GetEnergyRecoveryMax()
    {
        float result = 0.0f;

        //Базовое значение из Конфигурации
        result = config.energyRecovery[mkIndex];

        return result;
    }
    #endregion
}
