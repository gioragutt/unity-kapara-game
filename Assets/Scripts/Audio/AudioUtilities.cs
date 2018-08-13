using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public static class AudioUtilities
    {
        public static IEnumerator FadeOut(this AudioSource audioSource, float fadeTime)
        {
            if (audioSource == null)
            {
                yield break;
            }

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
