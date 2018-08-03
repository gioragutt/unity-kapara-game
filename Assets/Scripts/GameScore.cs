using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public PlayerMovement player;
    public Text scoreText;
    private string lastScore;
    private static readonly string SCORE_FORMAT = "Level: {0}\nScore: {1}";

    private void Start()
    {
        GameManager.GetGameManager().GameEnded += OnGameEnded;
    }

    private void OnGameEnded(object sender, System.EventArgs e)
    {
        scoreText.text = string.Format("Game Over! Score: {0}", lastScore);
    }

    private void Update()
    {
        if (player.enabled)
        {
            lastScore = CurrentPlayerZ();
            scoreText.text = string.Format(SCORE_FORMAT, GameManager.GetGameManager().CurrentLevel, lastScore);
        }
    }

    private string CurrentPlayerZ()
    {
        return player.transform.position.z.ToString("0");
    }
}
