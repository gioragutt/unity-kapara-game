using Assets.Scripts;
using UnityEngine;

public abstract class MenuKeyboardShortcuts : MonoBehaviour
{
    private void Start()
    {
        if (Utilities.Platform == Utilities.PlatformType.Mobile)
        {
            enabled = false;
            return;
        }

        AddShortcuts();
    }

    private void Update()
    {
        CheckForShortcutPressed();
    }

    protected abstract void AddShortcuts();
    protected abstract void CheckForShortcutPressed();
}
