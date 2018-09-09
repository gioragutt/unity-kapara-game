using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class CreditsKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public Credits credits;
        public Text restartButtonText;
        public Text mainMenuButtonText;

        public KeyCode restartKey = KeyCode.Return;
        public KeyCode mainMenuKey = KeyCode.Escape;

        protected override void AddShortcuts()
        {
            restartButtonText.AddKeyboardShortcutText(restartKey);
            mainMenuButtonText.AddKeyboardShortcutText(mainMenuKey);
        }

        protected override void CheckForShortcutPressed()
        {
            if (Input.GetKeyDown(restartKey))
                credits.RestartAtCheckpoint();
            if (Input.GetKeyDown(mainMenuKey))
                credits.OpenMainMenu();
        }
    }
}
