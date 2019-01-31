using System;
using UnityEngine;

namespace DllSky.Utility
{
    public class RendererController : MonoBehaviour
    {
        #region Variables        
        public Action<bool> OnVisibleToCamera;

        [SerializeField]
        private bool visibleToCamera;
        #endregion

        #region Unity methods
        //Объект виден камерой
        private void OnBecameVisible()
        {
            visibleToCamera = true;

            if (OnVisibleToCamera != null)
                OnVisibleToCamera.Invoke(true);
        }

        //Объект не виден камерами
        private void OnBecameInvisible()
        {
            visibleToCamera = false;

            if(OnVisibleToCamera != null)
                OnVisibleToCamera.Invoke(false);
        }
        #endregion

        #region Public methods
        public bool GetVisibleToCamera()
        {
            return visibleToCamera;
        }
        #endregion
    }
}
