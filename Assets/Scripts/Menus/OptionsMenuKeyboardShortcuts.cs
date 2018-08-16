using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuKeyboardShortcuts : MonoBehaviour
{
    public OptionsMenu menu;
    public Text resumeButtonText;

    public KeyCode resumeKey = KeyCode.Escape;

    private void Start()
    {
        resumeButtonText.AddKeyboardShortcutText(resumeKey);
    }

    private void Update()
    {
        if (Input.GetKey(resumeKey))
            menu.Resume();
    }
}
