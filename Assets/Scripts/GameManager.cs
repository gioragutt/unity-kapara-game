using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string levelName;
    public float restartDelay = 3f;
    public float endOfGameDelay = 3f;
    public GameObject completeLevelUi;
    public bool shouldBuildLevel = true;

    public event EventHandler GameEnded;
    public bool GameHasEnded { get; private set; }

    public static GameManager Get()
    {
        return FindObjectOfType<GameManager>();
    }

    #region Public API

    public void LoadNextLevel()
    {
        var currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneBuildIndex = currentSceneBuildIndex + 1;
        LoadScene(nextSceneBuildIndex);
    }

    public void CompleteLevel()
    {
        if (IsLastLevel)
            Invoke("ShowCredits", endOfGameDelay);
        else
            completeLevelUi.SetActive(true);
    }

    public void EndGame()
    {
        if (GameHasEnded)
        {
            return;
        }

        GameHasEnded = true;
        OnGameEnded();
        Invoke("ShowCredits", restartDelay);
    }

    public void RestartGame()
    {
        LoadScene(1);
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

    private bool IsLastLevel
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex ==
                SceneManager.sceneCountInBuildSettings - 2;
        }
    }

    private void ShowCredits()
    {
        LoadScene(SceneManager.sceneCountInBuildSettings - 1);
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
