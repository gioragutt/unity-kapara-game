using UnityEngine;

public class GameData : MonoBehaviour
{
    public float score;
    public float scoreAtCheckpoint;
    public int checkpointBuildIndex = -1;

    private void Start()
    {
        if (checkpointBuildIndex < 0)
            checkpointBuildIndex = GameManager.FirstLevelBuildIndex;
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
