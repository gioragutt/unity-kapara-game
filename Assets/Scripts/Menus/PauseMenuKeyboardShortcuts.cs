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

    private void Start()
    {
        resumeButtonText.AddKeyboardShortcutText(resumeKey);
        optionsButtonText.AddKeyboardShortcutText(optionsKey);
        startMenuButtonText.AddKeyboardShortcutText(startMenuKey);
    }

    private void Update()
    {
        if (Input.GetKey(resumeKey))
            pauseMenu.Resume();
        if (Input.GetKey(optionsKey))
            pauseMenu.OpenOptions();
        if (Input.GetKey(startMenuKey))
            pauseMenu.OpenStartMenu();
    }
}
