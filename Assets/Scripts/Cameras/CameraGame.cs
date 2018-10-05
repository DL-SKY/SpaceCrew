using DllSky.Patterns;
using UnityEngine;

public class CameraGame : Singleton<CameraGame>
{
    #region Variables
    private Camera thisCamera;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        thisCamera = GetComponent<Camera>();
    }
    #endregion

    #region Public methods
    public Camera GetCamera()
    {
        return thisCamera;
    }

    public void Resize(float _width, float _count)
    {
        thisCamera.orthographicSize = (0.5f * _width * _count) / thisCamera.aspect;
    }

    public void Reposition()
    {
        var offset = thisCamera.orthographicSize * 0.4f;
        transform.position += new Vector3(0.0f, offset, 0.0f);
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
