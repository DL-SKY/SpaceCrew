/*
© Alexander Danilovsky, 2016
----------------------------
= Скрипт локального вращения объекта =
*/

using UnityEngine;

namespace DllSky.Utility
{
    public class LocalRotate : MonoBehaviour
    {
        public float fSpeedOfRotation = 1.0f;

        private void LateUpdate()
        {
            //Локальное вращение объекта
            transform.Rotate(Vector3.forward, fSpeedOfRotation * Time.deltaTime);
        }
    }
}
