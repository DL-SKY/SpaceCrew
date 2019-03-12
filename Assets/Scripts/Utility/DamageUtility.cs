using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class DamageUtility
    {
        public static bool GetHit(ItemData _weapon, PointController _target, float _distance)
        {
            bool result = true;

            var accuracy = _weapon.GetSelfParameter(EnumParameters.accuracy);

            var optimalSize = _weapon.GetSelfParameter(EnumParameters.optimalSizeType);
            var targetSize = _target.destructible.SizeType;

            var optimalDistance = _weapon.GetSelfParameter(EnumParameters.optimalDistance);
            var targetDistance = _distance;

            Debug.LogWarning("Size: " + optimalSize + "/" + targetSize + "    Distance: " + optimalDistance + "/" + targetDistance);

            return result;
        }

        /*public static float GetCritical(float _dmg)
        {
            float result = 0.0f;

            return result;
        }*/
    }
}
