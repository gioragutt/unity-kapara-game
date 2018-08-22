using Assets.Scripts;
using Assets.Scripts.Menus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string FirstLevelSceneName = "Level1";
    public const string CreditsSceneName = "Credits";
    public const string OptionsMenuSceneName = "OptionsMenu";
    public const string PauseMenuSceneName = "PauseMenu";

    public string levelName;
    public float restartDelay = 3f;
    public float endOfGameDelay = 3f;
    public GameObject completeLevelUi;
    public bool shouldBuildLevel = true;

    private static readonly Stack<string> activeScenes = new Stack<string>();

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

    public void ShowStartMenu()
    {
        Time.timeScale = 1f;
        LoadScene(0);
    }

    public void ShowPauseMenu()
    {
        StartCoroutine(OpenAdditiveScene(PauseMenuSceneName));
    }

    public void ResumeFromPauseMenu()
    {
        CloseAdditiveScene(PauseMenuSceneName);
    }

    public void ShowOptionsMenu()
    {
        StartCoroutine(OpenAdditiveScene(OptionsMenuSceneName));
    }

    public void ResumeFromOptions()
    {
        CloseAdditiveScene(OptionsMenuSceneName);
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
            GameData.Instance.UpdateHighscore();
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
        ResetGameData();
    }

    private static void ResetGameData()
    {
        GameData.Instance.checkpointSceneName = FirstLevelSceneName;
        GameData.Instance.scoreAtCheckpoint = 0;
        GameData.Instance.score = 0;
    }

    #endregion Public API

    #region Implementation

    private IEnumerator OpenAdditiveScene(string sceneName)
    {
        Debug.LogFormat("OpenAdditiveScene(\"{0}\")", sceneName);
        Time.timeScale = 0;
        var current = SceneManager.GetActiveScene();
        ToggleKeyboardShortcuts(current);
        activeScenes.Push(current.name);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        yield return null; // wait for scene to load
        var nextScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(nextScene);
    }

    private void ToggleKeyboardShortcuts(Scene scene)
    {
        Debug.Log("Toggling keys for scene: \"" + scene.name + "\"");
        foreach (var go in scene.GetRootGameObjects())
        {
            var shortcuts = go.GetComponentsInChildren<MenuShortcuts>();
            if (shortcuts.Length == 0)
            {
                continue;
            }

            Debug.Log("-- GameObject: \"" + go.name + "\"");
            Array.ForEach(shortcuts, ToggleComponentEnabled);
        }
    }

    private static void ToggleComponentEnabled(MonoBehaviour component)
    {
        var nextValue = !component.enabled;
        Debug.LogFormat(
            "Toggling {0} ({1} -> {2})",
            component.name, 
            component.enabled, 
            nextValue);
        component.enabled = nextValue;
    }

    private void CloseAdditiveScene(string sceneName)
    {
        if (activeScenes.Count == 0)
        {
            Debug.LogWarningFormat("CloseAdditiveScene(\"{0}\") called without opening it first", sceneName);
            return;
        };

        Debug.LogFormat("CloseAdditiveScene(\"{0}\")", sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
        var previousScene = SceneManager.GetSceneByName(activeScenes.Pop());

        if (activeScenes.Count == 0)
        {
            Time.timeScale = 1f;
        }

        ToggleKeyboardShortcuts(previousScene);
        SceneManager.SetActiveScene(previousScene);
    }

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
