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
        var nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //var nextScene = SceneManager.GetSceneByBuildIndex(nextSceneBuildIndex);
        //LoadScene(nextScene.name);
        SceneManager.LoadScene(nextSceneBuildIndex);
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
        LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
        obstacles.GenerateObstacles();
        gameHasEnded = false;
    }
}
