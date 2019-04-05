using System.Collections;
using UnityEngine;

public interface IWeaponEffects
{
    void HideVFX();
    IEnumerator PrepareVFX(float _time);
    IEnumerator AttackVFX(Transform _start, IDestructible _end, float _time);
}