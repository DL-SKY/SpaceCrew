using DllSky.Patterns;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpaceship : LeanCameraZoom
{
    #region Variables
    [Space(10.0f)]
    public bool usePitch = false;
    public float Pitch;
    public float PitchSensitivity = 0.25f;
    public bool PitchClamp = true;
    public float PitchMin = -90.0f;
    public float PitchMax = 90.0f;

    [Space(10.0f)]
    public bool useYaw = true;
    public float Yaw;
    public float YawSensitivity = 0.25f;
    public bool YawClamp = false;
    public float YawMin = -45.0f;
    public float YawMax = 45.0f;
    #endregion

    #region Properties
    #endregion

    #region Unity Metods
    protected override void Start()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }

        if (Camera.transform.parent != transform)
        {
            Camera.transform.SetParent(transform);
            Camera.transform.localPosition = Vector3.zero;
            Camera.transform.localRotation = Quaternion.identity;
        }
    }

    protected override void LateUpdate()
    {
        // Make sure the camera exists
        if (LeanTouch.GetCamera(Camera, gameObject) == true)
        {
            // Get the fingers we want to use
            var fingers = LeanTouch.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount);
            // Get the pinch ratio of these fingers
            var pinchRatio = LeanGesture.GetPinchRatio(fingers, WheelSensitivity);
            // Modify the zoom value
            Zoom *= pinchRatio;
            if (ZoomClamp == true)
            {
                Zoom = Mathf.Clamp(Zoom, ZoomMin, ZoomMax);
            }

            //Масштаб
            SetZoom();

            // Get the scaled average movement vector of these fingers
            var drag = LeanGesture.GetScaledDelta(fingers);
            // Get base sensitivity
            var sensitivity = GetSensitivity();
            // Adjust pitch
            if (usePitch)
            {
                Pitch += drag.y * PitchSensitivity * sensitivity;
                if (PitchClamp == true)
                {
                    Pitch = Mathf.Clamp(Pitch, PitchMin, PitchMax);
                }
            }
            else
                Pitch = 0.0f;
            // Adjust yaw
            if (useYaw)
            {
                Yaw -= drag.x * YawSensitivity * sensitivity;
                if (YawClamp == true)
                {
                    Yaw = Mathf.Clamp(Yaw, YawMin, YawMax);
                }
            }
            else
                Yaw = 0.0f;

            //Поворот
            SetRotation();
        }
    }
    #endregion

    #region Public Metods
    #endregion

    #region Private Metods
    protected void SetZoom()
    {
        Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, -Zoom);
    }

    protected void SetRotation()
    {
        transform.localRotation = Quaternion.Euler(-Pitch, -Yaw, 0.0f);
    }

    private float GetSensitivity()
    {
        // Has a camera been set?
        if (Camera != null)
        {
            // Adjust sensitivity by FOV?
            if (Camera.orthographic == false)
            {
                return Camera.fieldOfView / 90.0f;
            }
        }

        return 1.0f;
    }
    #endregion

    #region Coroutines
    #endregion
}
