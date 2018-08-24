using UnityEngine;

namespace Assets.Scripts
{
    public class MobileGameControl : MonoBehaviour
    {
        protected GameOptions.MobileControlStyles mobileControlStyle;

        protected MobileGameControl(GameOptions.MobileControlStyles mobileControlStyle)
        {
            this.mobileControlStyle = mobileControlStyle;
        }

        void Start()
        {
            if (Utilities.Platform != Utilities.PlatformType.Mobile)
            {
                Destroy(gameObject);
                return;
            }

            ActivateIfGuiControl(GameOptions.MobileControlStyle);
            GameOptions.MobileControlStyleChanged += OnMobileControlStyleChanged;
        }

        private void OnMobileControlStyleChanged(object sender, GameOptions.MobileControlStyleChangedEventArgs e)
        {
            ActivateIfGuiControl(e.MobileControlStyle);
        }

        private void ActivateIfGuiControl(GameOptions.MobileControlStyles controlStyle)
        {
            gameObject.SetActive(controlStyle == mobileControlStyle);
        }
    }
}