using System.Collections;
using UnityEngine;
using Utility;

public class WeaponController : MonoBehaviour
{
    #region Variables
    [Header("Settings")]
    public Transform startPos;
    public float timeLerpRateToPrepare = 0.75f;
    public float timeLerpRateToAttack = 0.25f;

    [Space()]
    [SerializeField]
    protected bool isActive;
    [SerializeField]
    protected bool isShootPreparation;

    protected IWeaponEffects effects;

    [Space()]
    [SerializeField]
    protected ItemData data;
    [SerializeField]
    protected SpaceshipController spaceship;

    [SerializeField]
    protected PointController target;

    [SerializeField]
    protected EnumSizeType sizeType;
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
    private void Awake()
    {
        effects = GetComponent<IWeaponEffects>();
    }
    #endregion

    #region Public methods
    public virtual void Initialize(ItemData _data, SpaceshipController _spaceship)
    {
        data = _data;
        spaceship = _spaceship;

        ApplySelfParameters();

        isShootPreparation = false;

        ActivateWeapon();
    }    

    public virtual void ActivateWeapon()
    {
        isActive = true;

        if (!isShootPreparation)
        {
            StopAllCoroutines();
            StartCoroutine(TargetTracking());
        }
    }

    public virtual void DisableWeapon()
    {
        target = null;
        StopAllCoroutines();
        isActive = false;
        isShootPreparation = false;

        effects.HideVFX();
    }
    #endregion

    #region Protected methods
    protected virtual void ApplySelfParameters()
    {
        sizeType = (EnumSizeType)data.GetSelfParameter(EnumParameters.optimalSizeType);
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
            yield return Attack();
        }
    }

    protected virtual IEnumerator Preparation()
    {
        isShootPreparation = true;

        if (target == null)
        {
            effects.HideVFX();
            yield return new WaitForSeconds(rate);
        }
        else
        {
            yield return effects.PrepareVFX(rate * timeLerpRateToPrepare);
        }

        isShootPreparation = false;
    }

    protected virtual IEnumerator Attack()
    {
        //Подготовка к выстрелу
        yield return Preparation();

        GetTarget();

        if (target == null)
            yield break;

        var destructible = target.destructible;
        if (destructible == null)
            yield break;        

        //Попадание
        if (DamageUtility.CheckHit(data, target, Vector3.Distance(transform.position, target.transform.position)))
        {
            yield return effects.AttackVFX(startPos, destructible, rate * timeLerpRateToAttack);

            var damage = new Damage(armorDamage, shieldDamage, critical, accuracy);
            destructible.ApplyDamage(damage, startPos.position);
        }
        else
        {
            yield return effects.AttackVFX(startPos, destructible, rate * timeLerpRateToAttack, true);

            destructible.ApplyMiss(startPos.position);
        }        
    }
    #endregion

    #region Menu
    [ContextMenu("Activated")]
    private void MenuSetCameraTarget()
    {
        ActivateWeapon();
    }
    #endregion
}
