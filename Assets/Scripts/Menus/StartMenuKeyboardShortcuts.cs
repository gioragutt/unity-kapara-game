using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuKeyboardShortcuts : MenuKeyboardShortcuts
{
    public StartMenu startMenu;
    public Text startButtonText;
    public Text optionsButtonText;
    public Text quitButtonText;

    public KeyCode startKey = KeyCode.Return;
    public KeyCode optionsKey = KeyCode.O;
    public KeyCode quitKey = KeyCode.Escape;

    private void Start()
    {
        startButtonText.AddKeyboardShortcutText(startKey);
        optionsButtonText.AddKeyboardShortcutText(optionsKey);
        quitButtonText.AddKeyboardShortcutText(quitKey);
    }

    private void Update()
    {
        if (Input.GetKey(startKey))
            startMenu.StartGame();
        if (Input.GetKey(optionsKey))
            startMenu.OpenOptions();
        if (Input.GetKey(quitKey))
            startMenu.Quit();
    }
}
