using UnityEngine;
using UnityEngine.UI;

namespace DllSky.Utility
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectItemsScaler : MonoBehaviour
    {
        #region Vars
        public float multiplyer = 1;

        ScrollRect scrollRect;
        #endregion

        #region Unity methods
        void Start()
        {
            scrollRect = GetComponent<ScrollRect>();
        }

        void Update()
        {
            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                var child = scrollRect.content.GetChild(i);
                float dist = (child.transform.position - scrollRect.transform.position).magnitude;
                child.transform.localScale = new Vector3(1, 1, 1) * (1 - dist / Screen.width * multiplyer);
            }
        }
        #endregion
    }
}