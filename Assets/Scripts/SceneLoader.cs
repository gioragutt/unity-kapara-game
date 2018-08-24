using Assets.Scripts.Menus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        private readonly Stack<string> activeScenes = new Stack<string>();

        #region Implementation

        public void OpenAdditiveScene(string sceneName)
        {
            StartCoroutine(OpenAdditiveSceneCoroutine(sceneName));
        }

        public void CloseAdditiveScene(string sceneName)
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

        private IEnumerator OpenAdditiveSceneCoroutine(string sceneName)
        {
            Debug.LogFormat("OpenAdditiveScene(\"{0}\")", sceneName);
            Time.timeScale = 0;
            var current = SceneManager.GetActiveScene();
            ToggleKeyboardShortcuts(current);
            activeScenes.Push(current.name);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            yield return new WaitForEndOfFrame(); // wait for scene to load
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

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            var newScene = SceneManager.GetSceneByName(sceneName);
            Debug.Log("Loaded Scene: " + newScene.name);
        }

        public void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
            var newScene = SceneManager.GetSceneByBuildIndex(buildIndex);
            Debug.Log("Loaded Scene: " + newScene.name);
        }

        #endregion Implementation

        #region Singleton Component Implementation

        public static SceneLoader Instance
        {
            get; private set;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion Singleton Component Implementation
    }
}
