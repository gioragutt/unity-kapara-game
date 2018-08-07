using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Editor Variables

    public string levelName;
    public float restartDelay = 3f;
    public float endOfGameDelay = 3f;
    public event EventHandler GameEnded;
    public GameObject completeLevelUi;
    public ObstaclesGenerator obstacles;
    public bool shouldBuildLevel = true;

    #endregion Editor Variables

    #region Implementation Variable

    public bool GameHasEnded
    {
        get; private set;
    }

    #endregion

    public static GameManager Get()
    {
        return FindObjectOfType<GameManager>();
    }

    #region Unity Lifecycle

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #endregion Unity Lifecycle

    #region Public API

    public void LoadNextLevel()
    {
        var currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneBuildIndex = currentSceneBuildIndex + 1;
        Debug.LogFormat("Current Scene Index: {0}, Next Scene Index: {1}",
            currentSceneBuildIndex, nextSceneBuildIndex);
        SceneManager.LoadScene(nextSceneBuildIndex);
    }

    public void CompleteLevel()
    {
        if (!IsLastLevel)
            completeLevelUi.SetActive(true);
        else
            Invoke("LoadNextLevel", endOfGameDelay);
    }

    public void EndGame()
    {
        if (GameHasEnded)
        {
            return;
        }

        GameHasEnded = true;
        OnGameEnded();
        Invoke("RestartGame", restartDelay);
    }

    public void RestartGame()
    {
        LoadScene(0);
        GameData.Instance.score = 0;
    }

    #endregion Public API

    #region Implementation

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        var newScene = SceneManager.GetSceneByBuildIndex(buildIndex);
        Debug.Log("Loaded Scene: " + newScene.name);
        GameHasEnded = false;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (shouldBuildLevel)
            obstacles.GenerateObstacles();
    }

    private bool IsLastLevel
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex ==
                SceneManager.sceneCountInBuildSettings - 2;
        }
    }

    private void OnGameEnded()
    {
        if (GameEnded != null)
        {
            GameEnded.Invoke(null, null);
        }
    }

    #endregion Implementation
}
