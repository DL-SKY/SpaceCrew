using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponEffects
{
    void HideVFX();
    IEnumerator PrepareVFX(float _time);
    IEnumerator AttackVFX(Transform _start, Transform _end, float _time);
}