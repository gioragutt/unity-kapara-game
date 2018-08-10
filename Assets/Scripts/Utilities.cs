using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public static class Utilities
    {
        public static readonly Dictionary<KeyCode, string> keycodeNameOverrides = new Dictionary<KeyCode, string>
        {
            { KeyCode.Return, "Enter" }
        };

        public static void AddKeyboardShortcutText(this Text text, KeyCode shortcut)
        {
            string shortcutName;
            if (!keycodeNameOverrides.TryGetValue(shortcut, out shortcutName))
                shortcutName = shortcut.ToString();
            text.text += string.Format(" ({0})", shortcutName);
        }
    }
}
