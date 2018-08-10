using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuKeyboardShortcuts : MonoBehaviour
{
    public StartMenu startMenu;
    public Text startButtonText;
    public Text quitButtonText;

    public KeyCode startKey = KeyCode.Return;
    public KeyCode quitKey = KeyCode.Escape;

    private void Start()
    {
        startButtonText.AddKeyboardShortcutText(startKey);
        quitButtonText.AddKeyboardShortcutText(quitKey);
    }

    private void Update()
    {
        if (Input.GetKey(startKey))
            startMenu.StartGame();
        if (Input.GetKey(quitKey))
            startMenu.Quit();
    }
}
