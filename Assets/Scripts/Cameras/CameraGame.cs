using DllSky.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGame : Singleton<CameraGame>
{
    #region Variables

    private new Camera camera;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        camera = GetComponent<Camera>();
    }
    #endregion

    #region Public methods
    public Camera GetCamera()
    {
        return camera;
    }

    public void Resize(float _width, float _count)
    {
        camera.orthographicSize = (0.5f * _width * _count) / camera.aspect;
    }

    public void Reposition()
    {
        var offset = camera.orthographicSize * 0.4f;
        transform.position += new Vector3(0.0f, offset, 0.0f);
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
