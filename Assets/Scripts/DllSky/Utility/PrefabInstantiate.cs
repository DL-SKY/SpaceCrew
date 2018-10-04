/*
© Alexander Danilovsky, 2018
----------------------------
= Инстанцирует префаб =
*/

using UnityEngine;

namespace DllSky.Utility
{
    public class PrefabInstantiate : MonoBehaviour
    {
        #region Variables
        [Header("Parent")]
        public Transform parent;
        [Header("Prefab")]
        public GameObject prefab;
        #endregion

        #region Unity methods
        private void Awake()
        {
            if (prefab == null)
                return;

            if (parent == null)
                parent = transform;

            Instantiate(prefab, parent);
        }
        #endregion
    }
}