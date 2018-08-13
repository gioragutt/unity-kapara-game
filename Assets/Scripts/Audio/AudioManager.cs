using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public void Play(string name)
    {
        var requested = sounds.FirstOrDefault(s => s.name == name);
        if (requested != null)
        {
            requested.source.Play();
        }
        else
        {
            Debug.LogWarningFormat("Sound \"{0}\" not found", name);
        }
	}

    public void FadeOutAllPlayingSource(float fadeDuration)
    {
        foreach (var playingAudio in CurrentlyPlayingSounds)
            StartCoroutine(playingAudio.source.FadeOut(fadeDuration));
    }

    public void StopAllPlayingSounds()
    {
        foreach (var playingAudio in CurrentlyPlayingSounds)
            playingAudio.source.Stop();
    }

    private IEnumerable<Sound> CurrentlyPlayingSounds
    {
        get
        {
            return sounds.Where(s => s.source.isPlaying);
        }
    }

    private void AddSounds()
    {
        foreach (var sound in sounds)
            AddSound(sound);
    }

    private void AddSound(Sound sound)
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;
        sound.source = source;
    }

    #region Singleton Component Implementation

    public static AudioManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            AddSounds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton Component Implementation
}
