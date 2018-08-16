using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Get().ResumeFromPauseMenu();
    }

    public void OpenOptions()
    {
        GameManager.Get().ShowOptionsMenu();
    }

    public void OpenStartMenu()
    {
        GameManager.Get().ShowStartMenu();
    }
}
