using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class CreditsKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public Credits credits;
        public Text restartAtCheckpointButtonText;
        public Text restartButtonText;
        public Text startMenuButtonText;

        public KeyCode restartAtCheckpointKey = KeyCode.Return;
        public KeyCode restartKey = KeyCode.Backspace;
        public KeyCode startMenuKey = KeyCode.Escape;

        protected override void AddShortcuts()
        {
            restartAtCheckpointButtonText.AddKeyboardShortcutText(restartAtCheckpointKey);
            restartButtonText.AddKeyboardShortcutText(restartKey);
            startMenuButtonText.AddKeyboardShortcutText(startMenuKey);
        }

        protected override void CheckForShortcutPressed()
        {
            if (Input.GetKeyDown(restartAtCheckpointKey))
                credits.RestartAtCheckpoint();
            if (Input.GetKeyDown(restartKey))
                credits.Restart();
            if (Input.GetKeyDown(startMenuKey))
                credits.OpenStartMenu();
        }
    }
}
