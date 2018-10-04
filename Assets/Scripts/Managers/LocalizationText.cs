using DllSky.Managers;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Localization/LocalizationText")]
[RequireComponent(typeof(Text))]
public class LocalizationText : MonoBehaviour
{
    #region Variables
    public string key = "";

    private Text text;
    private string localizationString = "";
    #endregion

    #region Unity methods
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        ApplyLocalization();        
    }

    private void OnEnable()
    {
        EventManager.eventOnApplyLanguage += HandlerOnApplyLanguage;
    }

    private void OnDisable()
    {
        EventManager.eventOnApplyLanguage -= HandlerOnApplyLanguage;
    }
    #endregion

    #region Private methods
    private void HandlerOnApplyLanguage()
    {
        ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        if (!text)
            return;

        localizationString = LocalizationManager.Instance.Get(key);
        text.text = localizationString;
    }
    #endregion
}
