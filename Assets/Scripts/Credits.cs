using UnityEngine;

public class Credits : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Get().RestartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
