using Assets.Scripts.Audio;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public const string FirstLevelSceneName = "Level1";
        public const string CreditsSceneName = "Credits";
        public const string OptionsMenuSceneName = "OptionsMenu";
        public const string AboutMenuSceneName = "AboutMenu";
        public const string PauseMenuSceneName = "PauseMenu";

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

        private void Start()
        {
            if (shouldBuildLevel)
            {
                Debug.LogFormat("<GameManager> Starting \"{0}\"", levelName);
            }
        }

        #region Public API

        public void ShowMainMenu()
        {
            Time.timeScale = 1f;
            StartCoroutine(LoadScene(0));
        }

        public void ShowPauseMenu()
        {
            SceneLoader.Instance.OpenAdditiveScene(PauseMenuSceneName);
        }

        public void ResumeFromPauseMenu()
        {
            SceneLoader.Instance.CloseAdditiveScene(PauseMenuSceneName);
        }

        public void ShowOptionsMenu()
        {
            SceneLoader.Instance.OpenAdditiveScene(OptionsMenuSceneName);
        }

        public void ResumeFromOptions()
        {
            SceneLoader.Instance.CloseAdditiveScene(OptionsMenuSceneName);
        }

        public void ShowAbout()
        {
            StartCoroutine(SceneLoader.Instance.LoadScene(AboutMenuSceneName));
        }

        public void RestartAtCheckpoint()
        {
            GameData.Instance.score = GameData.Instance.scoreAtCheckpoint;
            StartCoroutine(LoadScene(GameData.Instance.checkpointSceneName));
        }

        public void LoadNextLevel()
        {
            var nextSceneBuildIndex = NextSceneBuildIndex;
            SaveCheckpoint(nextSceneBuildIndex);
            StartCoroutine(LoadScene(nextSceneBuildIndex));
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

        private void ShowCredits()
        {
            StartCoroutine(LoadScene(CreditsSceneName));
        }

        public void RestartGame()
        {
            StartCoroutine(LoadScene(FirstLevelSceneName));
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

        private static void SaveCheckpoint(int nextSceneBuildIndex)
        {
            if (GameData.Instance == null)
            {
                return;
            }

            GameData.Instance.scoreAtCheckpoint = GameData.Instance.score;
            GameData.Instance.checkpointSceneName = Utilities.NameOfSceneByBuildIndex(nextSceneBuildIndex);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return StartCoroutine(SceneLoader.Instance.LoadScene(sceneName));
            GameHasEnded = false;
        }

        private IEnumerator LoadScene(int buildIndex)
        {
            yield return StartCoroutine(SceneLoader.Instance.LoadScene(buildIndex));
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

        private void OnGameEnded()
        {
            if (GameEnded != null)
            {
                GameEnded.Invoke(null, null);
            }
        }

        #endregion Implementation
    }
}
