using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class OptionsMenu : MonoBehaviour
    {
        private static readonly Dictionary<int, GameOptions.MobileControlStyles> indexToControlStyle = new Dictionary<int, GameOptions.MobileControlStyles>
        {
            { 0, GameOptions.MobileControlStyles.Gui },
            { 1, GameOptions.MobileControlStyles.NoGui }
        };

        public Slider volume;
        public Dropdown graphicsQuality;
        public Dropdown controlStyle;

        private void Start()
        {
            InitVolume();
            InitGrapgicsQuality();
            InitMobileControlStyle();
        }

        private void InitVolume()
        {
            volume.value = GameOptions.Volume * volume.maxValue;
        }

        private void InitGrapgicsQuality()
        {
            graphicsQuality.options = QualitySettings.names.Select(name => new Dropdown.OptionData(name)).ToList();
            graphicsQuality.value = GameOptions.GraphicsQuality;
        }

        private void InitMobileControlStyle()
        {
            if (Utilities.Platform == Utilities.PlatformType.Mobile)
            {
                controlStyle.value = indexToControlStyle.First(
                    e => e.Value == GameOptions.MobileControlStyle).Key;
            }
        }

        public void ChangeMobileControlStyle(int selected)
        {
            GameOptions.MobileControlStyle = indexToControlStyle[selected];
        }

        public void ChangeQualityLevel(int selected)
        {
            GameOptions.GraphicsQuality = selected;
        }

        public void ChangeVolume(float value)
        {
            GameOptions.Volume = value / volume.maxValue;
        }

        public void Resume()
        {
            GameManager.Get().ResumeFromOptions();
        }
    }
}