using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public static class AudioUtilities
    {
        public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
        {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    }
}
