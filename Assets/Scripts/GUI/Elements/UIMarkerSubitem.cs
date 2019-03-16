using System.Collections;
using UnityEngine;

public class UIMarkerSubitem : MonoBehaviour
{
    #region Variables
    public UIMarkerSubitemAnimationConfig config;

    [Space()]
    public Transform center;
    public Transform content;
    public CanvasGroup canvas;

    [Space()]
    public RectTransform text;
    public CanvasGroup canvasText;
    #endregion

    #region Unity methods
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    #endregion

    #region Public methods
    public void Show()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ShowAnimation());
    }

    public void HideImediatly()
    {
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        StartCoroutine(HideAnimation());
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    private IEnumerator ShowAnimation()
    {
        var timer = 0.0f;
        var T = 0.0f;

        canvasText.alpha = 0.0f;

        //Основная кнопка
        while (T < 1.0)
        {
            T = Mathf.InverseLerp(0.0f, config.time, timer);

            content.position = Vector3.Lerp(center.position, transform.position, T);
            canvas.alpha = config.alpha.Evaluate(T);
            var scale = config.size.Evaluate(T);
            content.localScale = new Vector3(scale, scale, scale);

            yield return null;

            timer += Time.deltaTime;
        }

        //Текст
        timer = 0.0f;
        T = 0.0f;
        while (T < 1.0f)
        {
            T = Mathf.InverseLerp(0.0f, config.timeText, timer);

            text.anchoredPosition = new Vector2(config.deltaTextPosition.Evaluate(T), 0.0f);
            canvasText.alpha = config.alphaText.Evaluate(T);

            yield return null;

            timer += Time.deltaTime;
        }
    }

    private IEnumerator HideAnimation()
    {
        var timer = 0.0f;
        var T = 0.0f;

        //Текст
        canvasText.alpha = 0.0f;

        //Кнопка
        while (T < 1.0)
        {
            T = Mathf.InverseLerp(0.0f, config.time, timer);

            content.position = Vector3.Lerp(center.position, transform.position, 1-T);
            canvas.alpha = config.alpha.Evaluate(1-T);
            var scale = config.size.Evaluate(1-T);
            content.localScale = new Vector3(scale, scale, scale);

            yield return null;

            timer += Time.deltaTime;
        }

        gameObject.SetActive(false);
    }
    #endregion
}
