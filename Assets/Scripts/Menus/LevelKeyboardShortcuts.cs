using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class LevelKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public KeyCode pauseKey = KeyCode.Escape;

        protected override void AddShortcuts()
        {
        }

        protected override void CheckForShortcutPressed()
        {
            if (Input.GetKeyDown(pauseKey))
            {
                GameManager.Get().ShowPauseMenu();
            }
        }
    }
}
