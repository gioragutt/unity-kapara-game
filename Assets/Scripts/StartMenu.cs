using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Get().LoadNextLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
