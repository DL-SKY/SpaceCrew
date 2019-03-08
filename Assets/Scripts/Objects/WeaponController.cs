using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected ItemData data;
    [SerializeField]
    protected SpaceshipController spaceship;

    [SerializeField]
    protected PointController target;

    [SerializeField]
    protected float armorDamage;
    [SerializeField]
    protected float shieldDamage;
    [SerializeField]
    protected float optimalDistance;
    [SerializeField]
    protected float accuracy;
    [SerializeField]
    protected float critical;
    [SerializeField]
    protected float rate;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public virtual void Initialize(ItemData _data, SpaceshipController _spaceship)
    {
        data = _data;
        spaceship = _spaceship;

        ApplySelfParameters();
    }

    public virtual void ActivateWeapon()
    {
        StartCoroutine(TargetTracking());
    }

    public virtual void DisableWeapon()
    {
        target = null;
        StopAllCoroutines();
    }
    #endregion

    #region protected methods
    protected virtual void ApplySelfParameters()
    {
        armorDamage = data.GetSelfParameter(EnumParameters.armorDamage);
        shieldDamage = data.GetSelfParameter(EnumParameters.shieldDamage);
        optimalDistance = data.GetSelfParameter(EnumParameters.optimalDistance);
        accuracy = data.GetSelfParameter(EnumParameters.accuracy);
        critical = data.GetSelfParameter(EnumParameters.critical);
        rate = data.GetSelfParameter(EnumParameters.rate);
    }

    protected virtual void GetTarget()
    {
        if (spaceship.targets.Count < 1)
        {
            DisableWeapon();
        }
        else
        {
            //TODO
            target = spaceship.targets[0];
        }
    }
    #endregion

    #region Coroutines
    protected virtual IEnumerator TargetTracking()
    {
        while (true)
        {
            yield return StartCoroutine(Attack());
        }
    }

    protected virtual IEnumerator Attack()
    {
        GetTarget();

        if (target.type == EnumPointType.Enemy)
        {
            var enemy = target.GetComponent<SpaceshipController>();

            var damage = new Damage(armorDamage, shieldDamage);

            enemy.ApplyDamage(damage, transform.position);
        }

        yield return new WaitForSeconds(rate);
    }
    #endregion
}
