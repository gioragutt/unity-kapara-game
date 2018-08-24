using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(ObstaclesGenerator))]
    public class ObstaclesGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Regenerate Obstacles"))
                Script.GenerateObstaclesFromEditor();
            if (GUILayout.Button("Remove Obstacles"))
                Script.RemoveObstaclesFromEditor();
        }

        public ObstaclesGenerator Script
        {
            get
            {
                return (ObstaclesGenerator)target;
            }
        }
    }
}