using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class StartMenuKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public StartMenu startMenu;
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
                startMenu.StartGame();
            if (Input.GetKeyDown(optionsKey))
                startMenu.OpenOptions();
            if (Input.GetKeyDown(aboutKey))
                startMenu.OpenAbout();
            if (Input.GetKeyDown(quitKey))
                startMenu.Quit();
        }
    }
}
