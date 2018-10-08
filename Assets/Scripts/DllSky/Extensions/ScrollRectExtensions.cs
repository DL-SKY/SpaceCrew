using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DllSky.Extensions
{
    public static class ScrollRectExtensions
    {
        public static void ScrollToPosition(this ScrollRect scroller, float normalizePosition)
        {
            if (scroller.vertical)
            {
                var y = 0f;
                if (scroller.content.pivot.y == 1)
                    y = scroller.content.sizeDelta.y * normalizePosition;
                if (scroller.content.pivot.y == 0)
                    y = -scroller.content.sizeDelta.y * normalizePosition;
                scroller.content.anchoredPosition = new Vector2(scroller.content.anchoredPosition.x, y);
            }
            else
            {
                var x = -scroller.content.sizeDelta.x * normalizePosition;
                if (scroller.content.pivot.x == 1)
                    x += scroller.content.sizeDelta.x;
                scroller.content.anchoredPosition = new Vector2(x, scroller.content.anchoredPosition.y);
            }
        }

        public static IEnumerator ScrollToElementNextFrame(this ScrollRect scroller, int elementIndex, int totalCount)
        {
            float normalizePosition = 0f;
            if (scroller.horizontal)
            {
                normalizePosition = ((float)elementIndex - 0.5f) / (float)totalCount;
            }
            if (scroller.vertical)
            {
                normalizePosition = ((float)elementIndex - 1f) / (float)totalCount;
            }

            return ScrollToPositionNextFrame(scroller, normalizePosition);
        }

        public static IEnumerator ScrollToPositionNextFrame(this ScrollRect scroller, float normalizePosition)
        {
            // wait one frame to let content to calculate it's size
            yield return new WaitForEndOfFrame();

            scroller.ScrollToPosition(normalizePosition);
        }        
    }
}
