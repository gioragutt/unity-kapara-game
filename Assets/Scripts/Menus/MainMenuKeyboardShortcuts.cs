using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class MainMenuKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public MainMenu mainMenu;
        public Text startButtonText;
        public Text optionsButtonText;
        public Text aboutButtonText;
        public Text quitButtonText;

        public KeyCode startKey = KeyCode.Return;
        public KeyCode optionsKey = KeyCode.O;
        public KeyCode aboutKey = KeyCode.F1;
        public KeyCode quitKey = KeyCode.Escape;

        protected override void AddShortcuts()
        {
            startButtonText.AddKeyboardShortcutText(startKey);
            optionsButtonText.AddKeyboardShortcutText(optionsKey);
            aboutButtonText.AddKeyboardShortcutText(aboutKey);
            quitButtonText.AddKeyboardShortcutText(quitKey);
        }

        protected override void CheckForShortcutPressed()
        {
            if (Input.GetKeyDown(startKey))
                mainMenu.StartGame();
            if (Input.GetKeyDown(optionsKey))
                mainMenu.OpenOptions();
            if (Input.GetKeyDown(aboutKey))
                mainMenu.OpenAbout();
            if (Input.GetKeyDown(quitKey))
                mainMenu.Quit();
        }
    }
}
