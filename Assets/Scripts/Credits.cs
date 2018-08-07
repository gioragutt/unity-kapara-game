using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Text scoreText;
    public string scoreTextPrefix = "Score: ";

    private void Start()
    {
        scoreText.text = string.Format(
            "{0}{1}",
            scoreTextPrefix,
            GameData.Instance.score.ToIntegerString());
    }

    public void Restart()
    {
        GameManager.Get().RestartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
