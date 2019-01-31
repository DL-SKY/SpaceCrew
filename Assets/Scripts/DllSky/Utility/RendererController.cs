using System;
using UnityEngine;
using Utility;

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

        #region Context menu
        [ContextMenu("Create Convex Mesh")]
        private void CreateConvexMesh()
        {
            var mesh = GetComponent<MeshFilter>().mesh;
            var name = mesh.name;

            var convexMesh = MeshUtility.CreateMesh(mesh.vertices);

            UnityEditor.AssetDatabase.CreateAsset(convexMesh, string.Format(@"Assets/{0}_convex.asset", name));
        }
        #endregion
    }
}
