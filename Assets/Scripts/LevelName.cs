using UnityEngine;
using UnityEngine.UI;

public class LevelName : MonoBehaviour
{
    public Text levelNameText;

    void Update()
    {
        if (GameManager.Get().GameHasEnded)
            levelNameText.text = "";
        else
            levelNameText.text = GameManager.Get().levelName;
    }
}
