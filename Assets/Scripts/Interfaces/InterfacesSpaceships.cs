using UnityEngine;

public interface IUpdateRenderer
{
    void UpdateRenderer(SpaceshipMetadata _meta);
}

public interface IDestructible
{
    void ApplyDamage(Damage _damage, Vector3 _weaponPos);
    float GetShieldNormalize();
    float GetArmorNormalize();
}
