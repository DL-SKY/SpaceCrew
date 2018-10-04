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
        #region Delegates
        public delegate void OnDefault();
        public delegate void OnClickEsc();

        public delegate void OnChangeLanguage();
        public delegate void OnApplyLanguage();

        public delegate void OnStartPlayerTurn();
        public delegate void OnEndPlayerTurn();

        public delegate void OnChangeHitPoints();
        public delegate void OnChangePlayerSlots();
        #endregion

        #region Actions
        public static event OnDefault eventOnDefault;
        public static event OnClickEsc eventOnClickEsc;

        public static event OnChangeLanguage eventOnChangeLanguage;
        public static event OnApplyLanguage eventOnApplyLanguage;

        public static event OnStartPlayerTurn eventOnStartPlayerTurn;
        public static event OnEndPlayerTurn eventOnEndPlayerTurn;

        public static event OnChangeHitPoints eventOnChangeHitPoints;
        public static event OnChangePlayerSlots eventOnChangePlayerSlots;
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

        public static void CallOnStartPlayerTurn()
        {
            if (eventOnStartPlayerTurn != null)
                eventOnStartPlayerTurn.Invoke();
        }

        public static void CallOnEndPlayerTurn()
        {
            if (eventOnEndPlayerTurn != null)
                eventOnEndPlayerTurn.Invoke();
        }

        public static void CallOnChangeHitPoints()
        {
            if (eventOnChangeHitPoints != null)
                eventOnChangeHitPoints.Invoke();
        }

        public static void CallOnChangePlayerSlots()
        {
            if (eventOnChangePlayerSlots != null)
                eventOnChangePlayerSlots.Invoke();
        }
        #endregion
    }
}