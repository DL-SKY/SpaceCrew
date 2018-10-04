using UnityEngine;
using UnityEngine.UI;

namespace DllSky.Components
{
    public class ProgressBar : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float fillAmount;
        public float FillAmount
        {
            get { return fillAmount; }
            set { fillAmount = value; SetFill(); }
        }

        [SerializeField]
        private Image fillImage;
        #endregion

        #region Private methods
        private void SetFill()
        {
            fillImage.fillAmount = fillAmount;
        }
        #endregion
    }
}
