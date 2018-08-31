using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class StartMenu : MonoBehaviour
    {
        public enum StartSoundOption
        {
            StartMenuSound,
            LevelsMusic,
        }

        public StartSoundOption sound = StartSoundOption.StartMenuSound;

        public void Start()
        {
            AudioManager.Instance.StopAllPlayingSounds();
            AudioManager.Instance.Play(sound.ToString());
        }

        public void StartGame()
        {
            GameManager.Get().LoadNextLevel();
        }

        public void OpenOptions()
        {
            GameManager.Get().ShowOptionsMenu();
        }

        public void OpenAbout()
        {
            GameManager.Get().ShowAbout();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
