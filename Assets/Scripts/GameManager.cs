using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Editor Variables

    public float restartDelay = 3f;
    public event EventHandler GameEnded;
    public GameObject completeLevelUi;
    public ObstaclesGenerator obstacles;

    #endregion Editor Variables

    private bool gameHasEnded = false;

    public static GameManager Get()
    {
        return FindObjectOfType<GameManager>();
    }

    #region Public API

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

    #endregion Public API

    private void RestartGame()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Loading scene: " + currentSceneName);
        LoadScene(currentSceneName);
    }

    private void LoadScene(string currentSceneName)
    {
        SceneManager.LoadScene(currentSceneName);
        obstacles.GenerateObstacles();
        gameHasEnded = false;
    }
}
