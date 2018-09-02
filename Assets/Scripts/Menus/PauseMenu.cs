using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        public void Resume()
        {
            GameManager.Get().ResumeFromPauseMenu();
        }

        public void OpenOptions()
        {
            GameManager.Get().ShowOptionsMenu();
        }

        public void OpenMainMenu()
        {
            GameManager.Get().ShowMainMenu();
        }
    }
}
