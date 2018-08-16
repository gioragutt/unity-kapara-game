using Assets.Scripts;
using System.Collections;
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
    
    private void Start()
    {
        resumeButtonText.AddKeyboardShortcutText(resumeKey);
        optionsButtonText.AddKeyboardShortcutText(optionsKey);
        startMenuButtonText.AddKeyboardShortcutText(startMenuKey);
    }

    private void OnEnable()
    {
        Debug.Log("On Pause Menu Enabled");
        if (Input.GetKey(resumeKey))
        {
            allowResume = false;
        }
    }

    private void Update()
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
