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
            if (eventOnDefault != null)
                eventOnDefault.Invoke();
        }

        public static void CallOnClickEsc()
        {
            if (eventOnClickEsc != null)
                eventOnClickEsc.Invoke();
        }

        public static void CallOnChangeLanguage()
        {
            if (eventOnChangeLanguage != null)
                eventOnChangeLanguage.Invoke();
        }

        public static void CallOnApplyLanguage()
        {
            if (eventOnApplyLanguage != null)
                eventOnApplyLanguage.Invoke();
        }

        public static void CallOnResourceUpdate(string _resID)
        {
            if (eventOnResourceUpdate != null)
                eventOnResourceUpdate.Invoke(_resID);
        }

        public static void CallOnInitPointController(PointController _controller)
        {
            if (eventOnInitPointController != null)
                eventOnInitPointController.Invoke(_controller);
        }

        public static void CallOnShowMarkerSubtems(PointController _controller)
        {
            if (eventOnShowMarkerSubtems != null)
                eventOnShowMarkerSubtems.Invoke(_controller);
        }

        public static void CallOnPoint(PointController _controller, bool _selected)
        {
            if (eventOnPoint != null)
                eventOnPoint.Invoke(_controller, _selected);
        }

        public static void CallOnTargeting(PointController _controller, bool _selected)
        {
            if (eventOnTargeting != null)
                eventOnTargeting.Invoke(_controller, _selected);
        }

        public static void CallOnSetActiveTarget(PointController _controller, bool _selected)
        {
            if (eventOnSetActiveTarget != null)
                eventOnSetActiveTarget.Invoke(_controller, _selected);
        }

        public static void CallOnUpdateHitPoints(PointController _controller)
        {
            if (eventOnUpdateHitPoints != null)
                eventOnUpdateHitPoints.Invoke(_controller);
        }
        #endregion
    }
}