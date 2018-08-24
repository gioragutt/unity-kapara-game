using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts
{
    public class MobileGameControl : MenuShortcuts
    {
        protected GameOptions.MobileControlStyles mobileControlStyle;

        protected MobileGameControl(GameOptions.MobileControlStyles mobileControlStyle)
        {
            this.mobileControlStyle = mobileControlStyle;
        }

        void Start()
        {
            ActivateIfGuiControl(GameOptions.MobileControlStyle);
            GameOptions.MobileControlStyleChanged += OnMobileControlStyleChanged;
        }

        private void OnMobileControlStyleChanged(object sender, GameOptions.MobileControlStyleChangedEventArgs e)
        {
            ActivateIfGuiControl(e.MobileControlStyle);
        }

        private void ActivateIfGuiControl(GameOptions.MobileControlStyles controlStyle)
        {
            var isActive = controlStyle == mobileControlStyle;
            Debug.LogFormat("Setting {0} Active = {1}", GetType(), isActive);
            gameObject.SetActive(isActive);
        }
    }
}