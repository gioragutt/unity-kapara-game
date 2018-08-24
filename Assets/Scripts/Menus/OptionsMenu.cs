using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class OptionsMenu : MonoBehaviour
    {
        public Slider volume;
        public Dropdown graphicsQuality;

        private void Start()
        {
            volume.value = GameOptions.Volume * volume.maxValue;
            graphicsQuality.options = QualitySettings.names.Select(name => new Dropdown.OptionData(name)).ToList();
            graphicsQuality.value = GameOptions.GraphicsQuality;
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