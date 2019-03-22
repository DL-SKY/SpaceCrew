/*
© Alexander Danilovsky, 2018
----------------------------
= Event Manager =
*/

using System;

namespace DllSky.Managers
{
    public static class EventManager
    {
        #region Actions
        public static Action eventOnDefault;
        public static Action eventOnClickEsc;

        public static Action eventOnChangeLanguage;
        public static Action eventOnApplyLanguage;

        public static Action<string> eventOnResourceUpdate;

        public static Action<PointController> eventOnInitPointController;
        public static Action<PointController> eventOnShowMarkerSubtems;
        public static Action<PointController, bool> eventOnSetActiveTarget;
        public static Action<PointController, bool> eventOnPoint;
        public static Action<PointController, bool> eventOnTargeting;
        public static Action<PointController> eventOnUpdateHitPoints;
        #endregion

        #region Public methods
        public static void CallOnDefault()
        {
            eventOnDefault?.Invoke();
        }

        public static void CallOnClickEsc()
        {
            eventOnClickEsc?.Invoke();
        }

        public static void CallOnChangeLanguage()
        {
            eventOnChangeLanguage?.Invoke();
        }

        public static void CallOnApplyLanguage()
        {
            eventOnApplyLanguage?.Invoke();
        }

        public static void CallOnResourceUpdate(string _resID)
        {
            eventOnResourceUpdate?.Invoke(_resID);
        }

        public static void CallOnInitPointController(PointController _controller)
        {
            eventOnInitPointController?.Invoke(_controller);
        }

        public static void CallOnShowMarkerSubtems(PointController _controller)
        {
            eventOnShowMarkerSubtems?.Invoke(_controller);
        }

        public static void CallOnPoint(PointController _controller, bool _selected)
        {
            eventOnPoint?.Invoke(_controller, _selected);
        }

        public static void CallOnTargeting(PointController _controller, bool _selected)
        {
            eventOnTargeting?.Invoke(_controller, _selected);
        }

        public static void CallOnSetActiveTarget(PointController _controller, bool _selected)
        {
            eventOnSetActiveTarget?.Invoke(_controller, _selected);
        }

        public static void CallOnUpdateHitPoints(PointController _controller)
        {
            eventOnUpdateHitPoints?.Invoke(_controller);
        }
        #endregion
    }
}