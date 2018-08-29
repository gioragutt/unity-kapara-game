using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public static class Utilities
    {
        public enum PlatformType
        {
            Mobile,
            Standalone,
        }

        public static readonly PlatformType Platform = GetPlatformType();

        private static PlatformType GetPlatformType()
        {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
            return PlatformType.Standalone;
#else
            return PlatformType.Mobile;
#endif
        }

        public static readonly Dictionary<KeyCode, string> keycodeNameOverrides = new Dictionary<KeyCode, string>
        {
            { KeyCode.Return, "Enter" }
        };

        public static void AddKeyboardShortcutText(this Text text, KeyCode shortcut)
        {
            if (Platform == PlatformType.Mobile)
            {
                return;
            }

            string shortcutName;
            if (!keycodeNameOverrides.TryGetValue(shortcut, out shortcutName))
                shortcutName = shortcut.ToString();
            text.text += string.Format(" ({0})", shortcutName);
        }

        public static T ParseToEnum<T>(this string str)
        {
            return (T)System.Enum.Parse(typeof(T), str);
        }

        public static void ForEach<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        public static string NameOfSceneByBuildIndex(int buildIndex)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            int slash = path.LastIndexOf('/');
            string name = path.Substring(slash + 1);
            int dot = name.LastIndexOf('.');
            return name.Substring(0, dot);
        }

        public static List<T> FindObjectsOfType<T>()
        {
            var returned = new List<T>();
            var activeSceneCount = SceneManager.sceneCount;
            for (int i = 0; i < activeSceneCount; ++i)
            {
                var objectsInScene = SceneManager.GetSceneAt(i)
                    .GetRootGameObjects()
                    .SelectMany(g => g.GetComponentsInChildren<T>(true));
                returned.AddRange(objectsInScene);
            }
            return returned;
        }
    }
}
