using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Audio;
using Assets.Scripts.Options;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Start()
    {
        ChangeVolume(GameOptions.Volume);
        GameOptions.VolumeChanged += (_, args) => ChangeVolume(args.Volume);
    }

    private void ChangeVolume(float volume)
    {
        foreach (var s in sounds)
        {
            s.volume = s.originalVolume * volume;
            s.source.volume = s.volume;
        }
    }

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
        sound.originalVolume = sound.volume;
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
