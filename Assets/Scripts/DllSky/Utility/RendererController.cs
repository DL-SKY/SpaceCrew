﻿using System;
using System.Collections.Generic;
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
            var tStart = DateTime.Now;

            var mesh = GetComponent<MeshFilter>().sharedMesh;
            var name = mesh.name;

            List<Vector2> uv = new List<Vector2>();
            mesh.GetUVs(0, uv);

            var convexMesh = MeshUtility.CreateMesh(mesh);

            UnityEditor.AssetDatabase.CreateAsset(convexMesh, string.Format(@"Assets/{0}_convex.asset", name));

            Debug.Log("Convexing: " + (DateTime.Now - tStart).TotalSeconds);
        }
        #endregion
    }
}
