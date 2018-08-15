using Assets.Scripts.Options;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volume;

    private void Start()
    {
        volume.value = GameOptions.Volume;
    }

    public void ChangeVolume(float value)
    {
        GameOptions.Volume = value;
    }

    public void Resume()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
