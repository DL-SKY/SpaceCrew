using DllSky.Managers;
using DllSky.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    #region Variables
    [SerializeField]
    private string language = ConstantsLanguage.RUSSIAN;
    [SerializeField]
    private Dictionary<string, string> localization = new Dictionary<string, string>();
    #endregion

    #region Unity methods
    private void Start()
    {
        /*var lang = Application.systemLanguage;

        switch (lang)
        {
            case SystemLanguage.Russian:
                language = ConstantsLanguage.RUSSIAN;
                break;
            default:
                language = ConstantsLanguage.ENGLISH;
                break;
        }*/
    }

    private void OnEnable()
    {
        EventManager.eventOnChangeLanguage += HandlerOnChangeLanguage;
    }

    private void OnDisable()
    {
        EventManager.eventOnChangeLanguage -= HandlerOnChangeLanguage;
    }
    #endregion

    #region Public methods
    public string Get(string _key)
    {
        if (!localization.ContainsKey(_key))
            return _key;

        return localization[_key];
    }
    #endregion

    #region Private methods
    private void HandlerOnChangeLanguage()
    {
        ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        localization.Clear();        

        if (Global.IsInstantiated)
        {
            var global = Global.Instance;
            language = global.SETTINGS.language;

            for (int i = 0; i < global.CONFIGS.localization.Count; i++)
            {
                string key = global.CONFIGS.localization[i].id;
                string value = "";
                switch (language)
                {
                    case ConstantsLanguage.RUSSIAN:
                        value = global.CONFIGS.localization[i].rus;
                        break;
                    default:
                        value = global.CONFIGS.localization[i].eng;
                        break;
                }

                localization.Add(key, value);
            }
        }

        EventManager.CallOnApplyLanguage();
    }
    #endregion

    #region Context menu
    [ContextMenu("Change language (manual)")]
    private void ShowJSONSpaceShipData()
    {
        HandlerOnChangeLanguage();
    }
    #endregion
}
