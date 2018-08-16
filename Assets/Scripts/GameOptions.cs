using System;
using UnityEngine;

namespace Assets.Scripts.Options
{
    public static class GameOptions
    {
        #region Volume

        public const string VolumePerfString = "Volume";
        public static float Volume
        {
            get
            {
                return ReturnWithDefault(VolumePerfString, 1f);
            }
            set
            {
                var newVolume = Clamp(0f, 1f, value);
                if (newVolume == Volume)
                    return;

                PlayerPrefs.SetFloat(VolumePerfString, newVolume);
                OnVolumeChanged(newVolume);
            }
        }

        public class VolumeChangedEventArgs : EventArgs
        {
            public float Volume
            {
                get; set;
            }
        }

        public static event EventHandler<VolumeChangedEventArgs> VolumeChanged;

        private static void OnVolumeChanged(float volume)
        {
            if (VolumeChanged != null)
            {
                VolumeChanged.Invoke(null, new VolumeChangedEventArgs
                {
                    Volume = volume
                });
            }
        }

        #endregion Volume

        #region Utilities

        private static float Clamp(float min, float max, float value)
        {
            if (value > max)
                return max;
            if (value < min)
                return min;
            return value;
        }

        private static float ReturnWithDefault(string key, float defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetFloat(key, defaultValue);
            }
            return PlayerPrefs.GetFloat(key);
        }

        private static bool ReturnWithDefault(string key, bool defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                SetBool(key, defaultValue);
            }
            return GetBool(key);
        }

        private static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        private static bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) != 0;
        }

        #endregion
    }
}
