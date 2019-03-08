using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSpaceshipBase : MonoBehaviour
{
    #region Variables
#if UNITY_EDITOR
    public float Armor;
    public float Shield;
    public float Speed;    
#endif
    protected SpaceshipController spaceship;
    #endregion

    #region Unity methods
#if UNITY_EDITOR
    protected virtual void Update()
    {
        UpdateShowedParameters();
        UpdateInput();
    }
#endif
    #endregion

    #region Public methods
    public virtual void Initialize(SpaceshipController _spaceship)
    {
        if (_spaceship != null)
            spaceship = _spaceship;
        else
            spaceship = FindSpaceshipController();
    }

    public virtual void SetMaxSpeedType(EnumSpeedType _type)
    {
        spaceship.SetMaxSpeedType(_type);
    }

    public virtual void SetPoint(PointController _controller, bool _selected)
    {
        spaceship.SetPoint(_controller, _selected);
    }

    public virtual void SetTarget(PointController _controller, bool _selected)
    {
        spaceship.SetTarget(_controller, _selected);
    }
    #endregion

    #region Protected methods
    protected SpaceshipController FindSpaceshipController()
    {
        return GetComponent<SpaceshipController>();
    }

    protected virtual void UpdateInput()
    {

    }
    #endregion

    //------------------------------------------------------------------
    #region TEST
    protected virtual void UpdateShowedParameters()
    {
#if UNITY_EDITOR
        if (!spaceship)
            spaceship = FindSpaceshipController();

        Armor = spaceship.GetArmor();
        Shield = spaceship.GetShield();
        Speed = spaceship.GetSpeed();
#endif
    }    
    #endregion
    //------------------------------------------------------------------
}
