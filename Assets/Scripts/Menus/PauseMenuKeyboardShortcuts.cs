using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuKeyboardShortcuts : MenuKeyboardShortcuts
{
    public PauseMenu pauseMenu;
    public Text resumeButtonText;
    public Text optionsButtonText;
    public Text startMenuButtonText;

    public KeyCode resumeKey = KeyCode.Escape;
    public KeyCode optionsKey = KeyCode.O;
    public KeyCode startMenuKey = KeyCode.M;

    private bool allowResume = true;

    private void OnEnable()
    {
        Debug.Log("On Pause Menu Enabled");
        if (Input.GetKey(resumeKey))
        {
            allowResume = false;
        }
    }

    protected override void AddShortcuts()
    {
        resumeButtonText.AddKeyboardShortcutText(resumeKey);
        optionsButtonText.AddKeyboardShortcutText(optionsKey);
        startMenuButtonText.AddKeyboardShortcutText(startMenuKey);
    }

    protected override void CheckForShortcutPressed()
    {
        if (!allowResume)
        {
            StartCoroutine(WaitForResumeKeyToBeUnpressed());
        }

        if (allowResume && Input.GetKeyDown(resumeKey))
            pauseMenu.Resume();
        if (Input.GetKeyDown(optionsKey))
            pauseMenu.OpenOptions();
        if (Input.GetKeyDown(startMenuKey))
            pauseMenu.OpenStartMenu();
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
