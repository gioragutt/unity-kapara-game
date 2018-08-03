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
    private int _currentLevel = 1;

    public int CurrentLevel
    {
        get
        {
            return _currentLevel;
        }
    }

    public static GameManager GetGameManager()
    {
        return FindObjectOfType<GameManager>();
    }

    public void LoadNextLevel()
    {
        obstacles.rows++;
        obstacles.distanceBetweenRows -= 2;
        obstacles.maxGapSize -= 0.5f;
        obstacles.minGapSize -= 0.3f;
        _currentLevel++;
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

        _currentLevel = 1;
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
