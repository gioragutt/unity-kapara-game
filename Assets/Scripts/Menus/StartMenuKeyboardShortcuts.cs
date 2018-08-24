using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class StartMenuKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public StartMenu startMenu;
        public Text startButtonText;
        public Text optionsButtonText;
        public Text quitButtonText;

        public KeyCode startKey = KeyCode.Return;
        public KeyCode optionsKey = KeyCode.O;
        public KeyCode quitKey = KeyCode.Escape;

        protected override void AddShortcuts()
        {
            startButtonText.AddKeyboardShortcutText(startKey);
            optionsButtonText.AddKeyboardShortcutText(optionsKey);
            quitButtonText.AddKeyboardShortcutText(quitKey);
        }

        protected override void CheckForShortcutPressed()
        {
            if (Input.GetKeyDown(startKey))
                startMenu.StartGame();
            if (Input.GetKeyDown(optionsKey))
                startMenu.OpenOptions();
            if (Input.GetKeyDown(quitKey))
                startMenu.Quit();
        }
    }
}
