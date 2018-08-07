using UnityEngine;
using UnityEngine.UI;

public class CreditsKeyboardShortcuts : MonoBehaviour
{
    public Credits credits;
    public Text restartButtonText;
    public Text quitButtonText;
    public KeyCode restartKey = KeyCode.Space;
    public KeyCode quitKey = KeyCode.Escape;

    private void Start()
    {
        AppendShortcutHelperToButton(restartButtonText, restartKey);
        AppendShortcutHelperToButton(quitButtonText, quitKey);
    }

    private static void AppendShortcutHelperToButton(Text text, KeyCode shortcut)
    {
        text.text += string.Format(" ({0})", shortcut);
    }

    private void Update()
    {
        if (Input.GetKey(restartKey))
            credits.Restart();
        if (Input.GetKey(quitKey))
            credits.Quit();
    }
}
