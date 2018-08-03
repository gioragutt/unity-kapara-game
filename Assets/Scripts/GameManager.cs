using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 3f;
    public event EventHandler GameEnded;
    public GameObject completeLevelUi;
    public ObstaclesGenerator obstacles;

    private bool gameHasEnded = false;

    public static GameManager GetGameManager()
    {
        return FindObjectOfType<GameManager>();
    }

    public void LoadNextLevel()
    {
        RestartGame();
    }

    public void CompleteLevel()
    {
        completeLevelUi.SetActive(true);
    }

    public void EndGame()
    {
        if (gameHasEnded)
        {
            return;
        }

        gameHasEnded = true;
        GameEnded.Invoke(null, null);
        Invoke("RestartGame", restartDelay);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        obstacles.GenerateObstacles();
        gameHasEnded = false;
    }
}
