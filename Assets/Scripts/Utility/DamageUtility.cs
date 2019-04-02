using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class DamageUtility
    {
        public static bool CheckHit(ItemData _weapon, PointController _target, float _distance)
        {
            bool result = false;

            //Подготовка к просчету
            var accuracy = _weapon.GetSelfParameter(EnumParameters.accuracy);

            var optimalSize = _weapon.GetSelfParameter(EnumParameters.optimalSizeType);
            var targetSize = (int)_target.destructible.SizeType;

            var optimalDistance = _weapon.GetSelfParameter(EnumParameters.optimalDistance);
            var targetDistance = _distance;

            var lerpManeuver = _target.destructible.GetLerpManeuver();

            //Вероятность попадания
            var typesizeMod = targetSize - optimalSize;
            var distanceMod = GetHitChanceModDistance(targetDistance, optimalDistance);
            var chance = (accuracy + typesizeMod - lerpManeuver/2.0f) * distanceMod * 10.0f;
            chance = Mathf.Clamp(chance, 1.0f, 100.0f);

            //Результат
            var random = Random.Range(0.0f, 100.0f);
            result = random <= chance;

            Debug.Log(string.Format("[DamageUtility] Chance: {0} (distMod:{1})/ Result: {2}", chance, distanceMod, result));

            return result;
        }

        public static float GetHitChanceModDistance(float _distance, float _optimal)
        {
            //Близко
            if (_distance <= _optimal)
            {
                return Mathf.InverseLerp(0, _optimal/2, _distance);
            }
            //Далеко
            else
            {
                return Mathf.InverseLerp(_optimal*3, _optimal*2, _distance);
            }
        }

        public static float GetDamageValue(Damage _dmg, bool _isArmor)
        {
            var baseValue = _isArmor ? _dmg.ArmorDmg : _dmg.ShieldDmg;

            //Проверка на половинный урон ("касание")
            var chance = Mathf.Clamp(10.0f - _dmg.Accuracy, 1.0f, 10.0f) * 10.0f;
            if (Random.Range(0.0f, 100.0f) <= chance)
            {
                return baseValue * 0.5f;
            }

            //Проверка на критический урон
            chance = _dmg.Critical * 10.0f;
            if (Random.Range(0.0f, 100.0f) <= chance)
            {
                return baseValue * 2.0f;
            }

            return baseValue;
        }
    }
}
