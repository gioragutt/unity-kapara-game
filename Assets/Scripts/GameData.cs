using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public float score;
    public float scoreAtCheckpoint;
    public int checkpointBuildIndex = -1;

    private void Start()
    {
        if (checkpointBuildIndex >= 0)
            return;

        checkpointBuildIndex = GetInitialCheckpointBuildIndex();
    }

    private int GetInitialCheckpointBuildIndex()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < GameManager.FirstLevelBuildIndex ||
            currentScene > SceneManager.sceneCountInBuildSettings - 1)
        {
            return GameManager.FirstLevelBuildIndex;
        }

        return currentScene;
    }

    #region Implementation

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

    #endregion Implementation
}
