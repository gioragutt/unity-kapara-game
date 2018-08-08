﻿using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public void LoadNextLevel()
    {
        if (!GameManager.Get().GameHasEnded)
        {
            GameManager.Get().LoadNextLevel();
        }
    }
}
