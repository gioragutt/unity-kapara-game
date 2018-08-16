﻿using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public enum StartSoundOption
    {
        StartMenuSound,
        LevelsMusic,
    }

    public StartSoundOption sound = StartSoundOption.StartMenuSound;

    public void Start()
    {
        AudioManager.Instance.Play(sound.ToString());
    }

    public void StartGame()
    {
        GameManager.Get().LoadNextLevel();
    }

    public void OpenOptions()
    {
        GameManager.Get().ShowOptionsMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}