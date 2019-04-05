using UnityEngine;

public interface IUpdateRenderer
{
    void UpdateRenderer(SpaceshipMetadata _meta);
}

public interface IDestructible
{
    EnumSizeType SizeType { get; set; }

    Transform GetTransform();
    Vector3 GetPointToAttack(Vector3 _weaponPos);
    void ApplyDamage(Damage _damage, Vector3 _weaponPos);
    float GetLerpManeuver();
    float GetShieldNormalize();
    float GetArmorNormalize();
}
