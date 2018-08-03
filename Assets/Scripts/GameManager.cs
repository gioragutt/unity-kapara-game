using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 3f;
    public event EventHandler GameEnded;

    private bool gameHasEnded = false;

    public static GameManager GetGameManager()
    {
        return FindObjectOfType<GameManager>();
    }

    public void EndGame()
    {
        if (gameHasEnded)
        {
            return;
        }

        Debug.Log("GAME OVER!!");
        gameHasEnded = true;
        GameEnded.Invoke(null, null);
        Invoke("RestartGame", restartDelay);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
