using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class WaitForSceneToLoad : CustomYieldInstruction
    {
        private AsyncOperation sceneLoadOperation;
        public WaitForSceneToLoad(int buildIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            sceneLoadOperation = SceneManager.LoadSceneAsync(buildIndex, loadMode);
        }

        public WaitForSceneToLoad(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, loadMode);
        }

        public override bool keepWaiting
        {
            get
            {
                return !sceneLoadOperation.isDone;
            }
        }
    }
}
