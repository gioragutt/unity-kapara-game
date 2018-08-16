using Assets.Scripts.Options;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volume;

    private void Start()
    {
        volume.value = GameOptions.Volume * volume.maxValue;
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
