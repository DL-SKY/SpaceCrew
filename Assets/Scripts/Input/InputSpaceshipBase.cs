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
    private SpaceshipController spaceship;
    #endregion

    #region Unity methods
#if UNITY_EDITOR
    private void Update()
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
    #endregion

    #region Protected methods
    protected SpaceshipController FindSpaceshipController()
    {
        return GetComponent<SpaceshipController>();
    }

    protected virtual void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            SpeedUp();

        if (Input.GetKeyDown(KeyCode.S))
            SpeedDown();

        if (Input.GetKeyDown(KeyCode.Space))
            Stop();
    }
    #endregion

    //------------------------------------------------------------------
    #region TEST
    private void UpdateShowedParameters()
    {
#if UNITY_EDITOR
        if (!spaceship)
            spaceship = FindSpaceshipController();

        Armor = spaceship.GetArmor();
        Shield = spaceship.GetShield();
        Speed = spaceship.GetSpeed();
#endif
    }

    [ContextMenu("Speed Up +10%")]
    private void SpeedUp()
    {
        spaceship.AddSpeedNormalize(0.1f);
    }

    [ContextMenu("Speed Down -10%")]
    private void SpeedDown()
    {
        spaceship.AddSpeedNormalize(-0.1f);
    }

    [ContextMenu("Speed Full")]
    private void SpeedFull()
    {
        spaceship.SetSpeedNormalize(1.0f);
    }

    [ContextMenu("Stop")]
    private void Stop()
    {
        spaceship.SetSpeedNormalize(0.0f);
    }

    [ContextMenu("Rotate to Left")]
    private void RotateToLeft()
    {

    }

    [ContextMenu("Rotate to Right")]
    private void RotateToRight()
    {

    }
    #endregion
    //------------------------------------------------------------------
}
