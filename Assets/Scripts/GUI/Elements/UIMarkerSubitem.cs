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
    }

    private IEnumerator HideAnimation()
    {
        var timer = 0.0f;
        var T = 0.0f;

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
