using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public float score;
    public float scoreAtCheckpoint;
    public string checkpointSceneName = string.Empty;

    private void Start()
    {
        if (!string.IsNullOrEmpty(checkpointSceneName))
            return;

        checkpointSceneName = GetInitialCheckpoint();
    }

    private string GetInitialCheckpoint()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        return currentScene.StartsWith("Level") ? currentScene : GameManager.FirstLevelSceneName;
    }

    #region Singleton Component Implementation

    public static GameData Instance
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
