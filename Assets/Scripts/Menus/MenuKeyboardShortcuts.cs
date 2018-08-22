using Assets.Scripts;
using Assets.Scripts.Menus;

public abstract class MenuKeyboardShortcuts : MenuShortcuts
{
    private void Start()
    {
        if (Utilities.Platform == Utilities.PlatformType.Mobile)
        {
            Destroy(this);
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
