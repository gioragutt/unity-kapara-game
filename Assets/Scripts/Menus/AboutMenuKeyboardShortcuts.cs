using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class AboutMenuKeyboardShortcuts : MenuKeyboardShortcuts
    {
        public AboutMenu aboutMenu;
        public Text resumeButtonText;

        public KeyCode resumeKey = KeyCode.Escape;

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
        }

        protected override void CheckForShortcutPressed()
        {
            if (!allowResume)
            {
                StartCoroutine(WaitForResumeKeyToBeUnpressed());
            }

            if (allowResume && Input.GetKeyDown(resumeKey))
            {
                aboutMenu.Resume();
            }
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