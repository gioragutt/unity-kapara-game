using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuKeyboardShortcuts : MonoBehaviour
{
    public OptionsMenu menu;
    public Text optionsButtonText;

    public KeyCode optionsKey = KeyCode.Return;

    private void Start()
    {
        optionsButtonText.AddKeyboardShortcutText(optionsKey);
    }

    private void Update()
    {
        if (Input.GetKey(optionsKey))
            menu.Resume();
    }
}
