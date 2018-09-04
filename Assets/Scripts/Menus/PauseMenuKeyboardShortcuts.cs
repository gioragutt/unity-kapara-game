using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class PauseMenuKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public PauseMenu pauseMenu;
        public Text resumeButtonText;
        public Text restartAtCheckpointButtonText;
        public Text optionsButtonText;
        public Text mainMenuButtonText;

        public KeyCode resumeKey = KeyCode.Escape;
        public KeyCode restartAtCheckpointKey = KeyCode.Return;
        public KeyCode optionsKey = KeyCode.O;
        public KeyCode mainMenuKey = KeyCode.M;

        private bool allowResume = true;

        private void OnEnable()
        {
            if (Input.GetKey(resumeKey))
            {
                allowResume = false;
            }
        }

        protected override void AddShortcuts()
        {
            resumeButtonText.AddKeyboardShortcutText(resumeKey);
            restartAtCheckpointButtonText.AddKeyboardShortcutText(restartAtCheckpointKey);
            optionsButtonText.AddKeyboardShortcutText(optionsKey);
            mainMenuButtonText.AddKeyboardShortcutText(mainMenuKey);
        }

        protected override void CheckForShortcutPressed()
        {
            if (!allowResume)
            {
                StartCoroutine(WaitForResumeKeyToBeUnpressed());
            }

            if (allowResume && Input.GetKeyDown(resumeKey))
                pauseMenu.Resume();
            if (Input.GetKeyDown(restartAtCheckpointKey))
                pauseMenu.RestartAtCheckpoint();
            if (Input.GetKeyDown(optionsKey))
                pauseMenu.OpenOptions();
            if (Input.GetKeyDown(mainMenuKey))
                pauseMenu.OpenMainMenu();
        }

        private IEnumerator WaitForResumeKeyToBeUnpressed()
        {
            while (!Input.GetKeyUp(resumeKey))
            {
                yield return null;
            }
            allowResume = true;
        }
    }

}