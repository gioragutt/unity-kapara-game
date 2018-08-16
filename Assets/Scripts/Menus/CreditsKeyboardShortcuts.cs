using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class CreditsKeyboardShortcuts : MenuKeyboardShortcuts
{
    public Credits credits;
    public Text restartAtCheckpointButtonText;
    public Text restartButtonText;
    public Text quitButtonText;

    public KeyCode restartAtCheckpointKey = KeyCode.Return;
    public KeyCode restartKey = KeyCode.Backspace;
    public KeyCode quitKey = KeyCode.Escape;

    private void Start()
    {
        restartAtCheckpointButtonText.AddKeyboardShortcutText(restartAtCheckpointKey);
        restartButtonText.AddKeyboardShortcutText(restartKey);
        quitButtonText.AddKeyboardShortcutText(quitKey);
    }

    private void Update()
    {
        if (Input.GetKeyDown(restartAtCheckpointKey))
            credits.RestartAtCheckpoint();
        if (Input.GetKeyDown(restartKey))
            credits.Restart();
        if (Input.GetKeyDown(quitKey))
            credits.Quit();
    }
}
