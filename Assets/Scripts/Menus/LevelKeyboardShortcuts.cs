using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class LevelKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public PlayerMovement player;
        public KeyCode pauseKey = KeyCode.Escape;
        public KeyCode leftMovementKey = KeyCode.LeftArrow;
        public KeyCode rightMovementKey = KeyCode.RightArrow;

        protected override void AddShortcuts()
        {
        }

        protected override void CheckForShortcutPressed()
        {
            if (Input.GetKeyDown(pauseKey))
            {
                GameManager.Get().ShowPauseMenu();
            }
            player.IsMovingLeft = Input.GetKey(leftMovementKey);
            player.IsMovingRight = Input.GetKey(rightMovementKey);
        }
    }
}
