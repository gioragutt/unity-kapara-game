using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string FirstLevelSceneName = "Level1";
    private const string CreditsSceneName = "Credits";
    private const string OptionsMenuSceneName = "OptionsMenu";

    private string previousScene;

    public string levelName;
    public float restartDelay = 3f;
    public float endOfGameDelay = 3f;
    public GameObject completeLevelUi;
    public bool shouldBuildLevel = true;

    public event EventHandler GameEnded;
    public bool GameHasEnded
    {
        get; private set;
    }

    public static GameManager Get()
    {
        return FindObjectOfType<GameManager>();
    }

    #region Public API

    public void ShowOptionsMenu()
    {
        Time.timeScale = 0;
        //previousScene = SceneManager.GetActiveScene().name;
        var current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(OptionsMenuSceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(current);
    }

    public void ResumeFromOptions()
    {
        Time.timeScale = 1f;
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(previousScene));
        SceneManager.UnloadSceneAsync(OptionsMenuSceneName);
    }

    public void RestartAtCheckpoint()
    {
        GameData.Instance.score = GameData.Instance.scoreAtCheckpoint;
        LoadScene(GameData.Instance.checkpointSceneName);
    }

    public void LoadNextLevel()
    {
        var nextSceneBuildIndex = NextSceneBuildIndex;
        LoadScene(nextSceneBuildIndex);
        SaveCheckpoint(nextSceneBuildIndex);
    }

    public void CompleteLevel()
    {
        if (IsLastLevel(NextSceneBuildIndex))
        {
            Invoke("ShowCredits", endOfGameDelay);
        }
        else
        {
            completeLevelUi.SetActive(true);
        }
    }

    public void EndGame()
    {
        if (GameHasEnded)
        {
            return;
        }

        AudioManager.Instance.FadeOutAllPlayingSource(1f);
        GameHasEnded = true;
        OnGameEnded();
        Invoke("ShowCredits", restartDelay);
    }

    public void RestartGame()
    {
        LoadScene(FirstLevelSceneName);
        GameData.Instance.checkpointSceneName = FirstLevelSceneName;
        GameData.Instance.scoreAtCheckpoint = 0;
        GameData.Instance.score = 0;
    }

    #endregion Public API

    #region Implementation

    private void SaveCheckpoint(int nextSceneBuildIndex)
    {
        if (GameData.Instance == null)
        {
            return;
        }

        GameData.Instance.scoreAtCheckpoint = GameData.Instance.score;
        GameData.Instance.checkpointSceneName = Utilities.NameOfSceneByBuildIndex(nextSceneBuildIndex);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        var newScene = SceneManager.GetSceneByName(sceneName);
        Debug.Log("Loaded Scene: " + newScene.name);
        GameHasEnded = false;
    }

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        var newScene = SceneManager.GetSceneByBuildIndex(buildIndex);
        Debug.Log("Loaded Scene: " + newScene.name);
        GameHasEnded = false;
    }

    private bool IsLastLevel(int nextBuildIndex)
    {
        return Utilities.NameOfSceneByBuildIndex(nextBuildIndex) == CreditsSceneName;
    }

    private int NextSceneBuildIndex
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex + 1;
        }
    }

    private void ShowCredits()
    {
        LoadScene(CreditsSceneName);
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
