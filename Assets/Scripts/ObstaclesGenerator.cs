using Assets.Scripts.Obstacles;
using UnityEngine;
using System.Linq;
using Assets.Scripts;

public class ObstaclesGenerator : MonoBehaviour
{
    public int distanceToEndFromLastRow = 10;
    public float distanceBetweenObstacleSegments = 60;

    public Transform ground;
    public Transform player;
    public Transform obstaclePrefab;
    public Transform endGamePrefab;
    public TextAsset obstaclesDefinition;

    void Start()
    {
        transform.position = player.transform.position + Vector3.forward * distanceBetweenObstacleSegments;
        GenerateObstacles();
    }

    #region Editor Methods

    public void GenerateObstaclesFromEditor()
    {
        RemoveObstaclesFromEditor();
        CreateObstacles();
    }

    public void RemoveObstaclesFromEditor()
    {
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }

    #endregion Editor Methods

    public void GenerateObstacles()
    {
        RemoveObstacles();
        CreateObstacles();
    }

    private void CreateObstacles()
    {
        var lastObstaclePosition = CreateAllObstacles();
        CreateEndGame(lastObstaclePosition);
    }

    private Vector3 CreateAllObstacles()
    {
        var factory = new ObstaclesFactory(ground, player);
        var obstacleGeneartors = factory.Create(obstaclesDefinition.text);
        var lastObstaclePosition = transform.position;
        bool firstGenerator = true;
        foreach (var generator in obstacleGeneartors)
        {
            var startingPosition = lastObstaclePosition;
            if (firstGenerator)
                firstGenerator = false;
            else
                startingPosition = startingPosition + Vector3.forward * distanceBetweenObstacleSegments;

            var obstacles = generator.Generate(startingPosition);
            foreach (var obstacle in obstacles)
                CreateObstacle(obstacle);
            lastObstaclePosition = obstacles.Max(o => o.Position.z) * Vector3.forward;
        }

        return lastObstaclePosition;
    }

    private void CreateEndGame(Vector3 lastObstaclePosition)
    {
        var position = lastObstaclePosition + Vector3.forward * distanceToEndFromLastRow;
        Instantiate(endGamePrefab, position, Quaternion.identity, transform);
    }

    private void RemoveObstacles()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject != null)
                Destroy(child.gameObject);
        }
    }

    private void CreateObstacle(Obstacle obstacleasd)
    {
        var obstacle = Instantiate(
            obstaclePrefab,
            obstacleasd.Position,
            Quaternion.identity,
            transform);

        obstacle.localScale = Vector3.Scale(obstacle.localScale, new Vector3(obstacleasd.Width, 1, 1));
    }
}
