using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuKeyboardShortcuts : MenuKeyboardShortcuts
{
    public OptionsMenu menu;
    public Text resumeButtonText;

    public KeyCode resumeKey = KeyCode.Escape;

    protected override void AddShortcuts()
    {
        resumeButtonText.AddKeyboardShortcutText(resumeKey);
    }

    protected override void CheckForShortcutPressed()
    {
        if (Input.GetKeyDown(resumeKey))
        {
            menu.Resume();
        }
    }
}
