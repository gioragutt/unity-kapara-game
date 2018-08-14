using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    private const string HighscorePerfsKey = "Highscore";

    public PlayerMovement player;
    public Text scoreText;

    private float lastPlayerPosition;

    private void Start()
    {
        GameManager.Get().GameEnded += OnGameEnded;
        lastPlayerPosition = CurrentPlayerZ;
    }

    private void OnGameEnded(object sender, System.EventArgs e)
    {
        var score = GameData.Instance.score;
        var highscore = UpdateHighscore(score);
        scoreText.text = string.Format(
            "Game Over!\nScore: {0}\nHigh Score: {1}",
            score.ToIntegerString(),
            highscore.ToIntegerString());
    }

    private static float UpdateHighscore(float score)
    {
        var highscore = PlayerPrefs.GetFloat(HighscorePerfsKey, -1);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetFloat(HighscorePerfsKey, highscore);
        }

        return highscore;
    }

    private void FixedUpdate()
    {
        if (player != null && player.enabled)
        {
            UpdatePlayerScore();
            scoreText.text = GameData.Instance.score.ToIntegerString();
        }
    }

    private void UpdatePlayerScore()
    {
        var distancePassed = CurrentPlayerZ - lastPlayerPosition;
        if (distancePassed > 0)
            GameData.Instance.score += distancePassed;
        lastPlayerPosition = CurrentPlayerZ;
    }

    private float CurrentPlayerZ
    {
        get
        {
            return player.transform.position.z;
        }
    }
}
