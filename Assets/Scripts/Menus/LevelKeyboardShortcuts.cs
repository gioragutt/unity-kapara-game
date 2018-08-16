using UnityEngine;

public class LevelKeyboardShortcuts : MenuKeyboardShortcuts
{
    public KeyCode pauseKey = KeyCode.Escape;

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
            GameManager.Get().ShowPauseMenu();
    }
}
