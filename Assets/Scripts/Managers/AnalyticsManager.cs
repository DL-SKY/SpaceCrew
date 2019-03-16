/* ------------------------------------------------------------------- *\
 * МЕНЕДЖЕР 
 * для отправки аналитических метрик
 * UNITY ANALYSTICS
 * 
 * https://docs.unity3d.com/Manual/UnityAnalyticsStandardEvents.html
 * https://docs.unity3d.com/ScriptReference/Analytics.AnalyticsEvent.html
 * 
 \* ------------------------------------------------------------------ */

using DllSky.Patterns;
using System.Collections.Generic;
using UnityEngine.Analytics;

namespace DllSky.Analytics
{
    public class AnalyticsManager : Singleton<AnalyticsManager>
    {
        #region Variables
        #endregion

        #region Unity methods
        #endregion

        #region Public methods
        public void SendEvent(EnumAnalyticsEventType _event, object _data)
        {
            switch (_event)
            {
                case EnumAnalyticsEventType.NA:
                    break;



                case EnumAnalyticsEventType.Log:
                    break;

                case EnumAnalyticsEventType.Warning:
                    break;

                case EnumAnalyticsEventType.Error:
                    break;



                case EnumAnalyticsEventType.GameStart:
                    AnalyticsEvent.GameStart();
                    //AnalyticsEvent.Custom("GameStart");
                    break;

                case EnumAnalyticsEventType.GameOver:
                    var gameOverData = (AnaliticsGameOverData)_data;
                    AnalyticsEvent.GameOver(gameOverData.name, gameOverData.eventData);
                    //AnalyticsEvent.Custom("GameOver", gameOverData.eventData);
                    break;

                case EnumAnalyticsEventType.GamePause:
                    var gamePauseData = (AnaliticsGameOverData)_data;
                    AnalyticsEvent.Custom("game_pause", gamePauseData.eventData);
                    break;



                case EnumAnalyticsEventType.ScreenVisit:
                    var screenVisitData = (AnalyticsScreenVisitData)_data;
                    AnalyticsEvent.ScreenVisit(screenVisitData.nextScreenName, screenVisitData.eventData);
                    break;



                case EnumAnalyticsEventType.StoreOpened:
                    AnalyticsEvent.StoreOpened((StoreType)_data);
                    break;
            }
        }
        #endregion

        #region Private methods
        #endregion
    }

    public enum EnumAnalyticsEventType
    {
        NA,

        Log,
        Warning,
        Error,

        GameStart,              //game_start
        GameOver,               //game_over 
        GamePause,              //game_pause

        //Настройки

        //Туториал

        //Интерфейс
        ScreenVisit,

        //Социальная составляющая

        //Монетизация
        StoreOpened,
    }

    public class AnaliticsGameOverData
    {
        public string name;
        public Dictionary<string, object> eventData;

        public AnaliticsGameOverData(int _sessionTime, string _name = null)
        {
            name = _name;
            var value = "";

            if (_sessionTime < 1)
                value = "< 1 min";
            else if (_sessionTime < 5)
                value = "< 5 min";
            else if (_sessionTime < 10)
                value = "< 10 min";
            else if (_sessionTime < 20)
                value = "< 20 min";
            else
                value = "> 20 min";

            eventData = new Dictionary<string, object> { { "session_time", value } };
        }
    }

    public class AnalyticsScreenVisitData
    {
        public string nextScreenName;
        public Dictionary<string, object> eventData;

        public AnalyticsScreenVisitData(string _nextScreenName, string _prevScreenName)
        {
            nextScreenName = _nextScreenName;
            eventData = new Dictionary<string, object> { { "from_screen", _prevScreenName } };
        }
    }
}
