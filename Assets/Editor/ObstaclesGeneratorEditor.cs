using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstaclesGenerator))]
public class ObstaclesGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate Obstacles"))
            Script.GenerateObstacles();
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
