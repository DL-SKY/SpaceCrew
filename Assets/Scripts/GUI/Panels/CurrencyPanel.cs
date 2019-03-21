using DllSky.Managers;
using DllSky.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyPanel : MonoBehaviour
{
    #region Variables
    [Header("Credits")]
    public float timeCreditsAnimation = 1.0f;
    public Text creditsText;
    private int oldCreditsValue;
    private Coroutine creditsCoroutine;

    [Header("Tokens")]
    public float timeTokensAnimation = 1.0f;
    public Text tokensText;
    private int oldTokensValue;
    private Coroutine tokensCoroutine;
    #endregion

    #region Unity methods
    private void Start()
    {
        UpdateCurrency(false);
    }

    private void OnEnable()
    {
        EventManager.eventOnResourceUpdate += HandlerOnResourceUpdate;

        UpdateCurrency();
    }

    private void OnDisable()
    {
        EventManager.eventOnResourceUpdate -= HandlerOnResourceUpdate;
    }
    #endregion

    #region Public methods
    public void UpdateCurrency(bool _withAnimation = true)
    {
        StopAllCoroutines();
        creditsCoroutine = null;
        tokensCoroutine = null;

        int cr = Global.Instance.PROFILE.GetItem(ConstantsResourcesID.CREDITS);
        int tk = Global.Instance.PROFILE.GetItem(ConstantsResourcesID.TOKENS);

        if (_withAnimation)
        {
            creditsCoroutine = StartCoroutine(UpdateCredits(cr));
            tokensCoroutine = StartCoroutine(UpdateTokens(tk));
        }
        else
        {
            oldCreditsValue = cr;
            oldTokensValue = tk;

            ShowCredits();
            ShowTokens();
        }
    }
    #endregion

    #region Private methods
    private void HandlerOnResourceUpdate(string _resID)
    {
        if (_resID == ConstantsResourcesID.CREDITS)
        {
            int cr = Global.Instance.PROFILE.GetItem(ConstantsResourcesID.CREDITS);

            if (creditsCoroutine != null)
                StopCoroutine(creditsCoroutine);
            creditsCoroutine = StartCoroutine(UpdateCredits(cr));
        }
        else if (_resID == ConstantsResourcesID.TOKENS)
        {
            int tk = Global.Instance.PROFILE.GetItem(ConstantsResourcesID.TOKENS);

            if (tokensCoroutine != null)
                StopCoroutine(tokensCoroutine);
            tokensCoroutine = StartCoroutine(UpdateTokens(tk));
        }
    }

    private void ShowCredits()
    {
        creditsText.text = UtilityBase.GetStringFormatAmount6(oldCreditsValue);
    }

    private void ShowTokens()
    {
        tokensText.text = UtilityBase.GetStringFormatAmount6(oldTokensValue);
    }
    #endregion

    #region Coroutines
    private IEnumerator UpdateCredits(int _max)
    {
        var time = 0.0f;
        var T = 0.0f;
        var min = oldCreditsValue;

        while (T < 1.0f)
        {
            T = Mathf.InverseLerp(0.0f, timeCreditsAnimation, time);

            oldCreditsValue = (int)Mathf.Lerp(min, _max, T);
            ShowCredits();

            yield return null;
            time += Time.deltaTime;
        }
    }

    private IEnumerator UpdateTokens(int _max)
    {
        var time = 0.0f;
        var T = 0.0f;
        var min = oldTokensValue;

        while (T < 1.0f)
        {
            T = Mathf.InverseLerp(0.0f, timeTokensAnimation, time);

            oldTokensValue = (int)Mathf.Lerp(min, _max, T);
            ShowTokens();

            yield return null;
            time += Time.deltaTime;
        }
    }
    #endregion

    #region Menu
    [ContextMenu("Add credits")]
    private void MenuAddCredits()
    {
        Global.Instance.PROFILE.AddItem(ConstantsResourcesID.CREDITS, 1000);
    }

    [ContextMenu("Remove credits")]
    private void MenuRemoveCredits()
    {
        Global.Instance.PROFILE.AddItem(ConstantsResourcesID.CREDITS, -500);
    }

    [ContextMenu("Add tokens")]
    private void MenuAddTokens()
    {
        Global.Instance.PROFILE.AddItem(ConstantsResourcesID.TOKENS, 10);
    }

    [ContextMenu("Remove tokens")]
    private void MenuRemoveTokens()
    {
        Global.Instance.PROFILE.AddItem(ConstantsResourcesID.TOKENS, -5);
    }
    #endregion
}
