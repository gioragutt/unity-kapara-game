using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class GenericMenuButton : MonoBehaviour
    {
        public enum MenuActionType
        {
            PauseGame,
            GoToAbout,
            GoToOptions,
            Resume,
            StartGameClean,
            StartGameCheckpoint,
            GoToMainMenu,
            QuitGame,
        }

        public string text;
        public KeyCode shortcut;
        public MenuActionType type;
        public bool disableOnMobile = true;

        private Button button;
        private Text buttonText;
        private bool wasButtonPressed = false;
        private static readonly bool IsMobile = Utilities.Platform == Utilities.PlatformType.Mobile;

        void Start()
        {
            button = GetComponent<Button>();
            buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = text;
            buttonText.AddKeyboardShortcutText(shortcut);
        }

        public void ButtonPressed()
        {
            PerformMenuAction();
        }

        void Update()
        {
            if (IsMobile && disableOnMobile)
            {
                return;
            }

            if (wasButtonPressed)
            {
                Debug.LogFormat("<GenericMenuButton ({0})> Already pressed, ignoring", type);
                return;
            }

            if (Input.GetKeyDown(shortcut))
            {
                wasButtonPressed = true;
                Debug.LogFormat("<GenericMenuButton ({0})> Pressed", type);
                PerformMenuAction();
            }
        }

        private void PerformMenuAction()
        {
            switch (type)
            {
                case MenuActionType.PauseGame:
                    GameManager.Get().ShowPauseMenu();
                    break;
                case MenuActionType.GoToAbout:
                    GameManager.Get().ShowAbout();
                    break;
                case MenuActionType.GoToOptions:
                    GameManager.Get().ShowOptionsMenu();
                    break;
                case MenuActionType.Resume:
                    // Todo: Resume should be generic, not rely on which menu called it
                    GameManager.Get().ResumeFromOptions();
                    break;
                case MenuActionType.StartGameClean:
                    GameManager.Get().StartGameClean();
                    break;
                case MenuActionType.StartGameCheckpoint:
                    GameManager.Get().RestartAtCheckpoint();
                    break;
                case MenuActionType.GoToMainMenu:
                    GameManager.Get().ShowMainMenu();
                    break;
                case MenuActionType.QuitGame:
                    Application.Quit();
                    break;
            }
        }
    }
}
