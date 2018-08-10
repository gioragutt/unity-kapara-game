using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class CreditsKeyboardShortcuts : MonoBehaviour
{
    public Credits credits;
    public Text restartButtonText;
    public Text quitButtonText;

    public KeyCode restartKey = KeyCode.Return;
    public KeyCode quitKey = KeyCode.Escape;

    private void Start()
    {
        restartButtonText.AddKeyboardShortcutText(restartKey);
        quitButtonText.AddKeyboardShortcutText(quitKey);
    }

    private void Update()
    {
        if (Input.GetKey(restartKey))
            credits.Restart();
        if (Input.GetKey(quitKey))
            credits.Quit();
    }
}
