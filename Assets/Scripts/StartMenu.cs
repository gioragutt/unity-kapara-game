using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void Start()
    {
        AudioManager.Instance.Play("StartMenuSound");
    }

    public void StartGame()
    {
        GameManager.Get().LoadNextLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
