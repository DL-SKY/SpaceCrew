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
        #endregion
    }
}