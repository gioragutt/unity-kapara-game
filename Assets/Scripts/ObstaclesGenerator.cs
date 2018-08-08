using Assets.Scripts.Obstacles;
using UnityEngine;
using System.Linq;
using Assets.Scripts;

public class ObstaclesGenerator : MonoBehaviour
{
    public int distanceToEndFromLastRow = 10;
    public int rows = 5;
    public float distanceBetweenRows = 18;
    public float minGapSize = 3;
    public float maxGapSize = 5;
    public float minimumObstacleWidth = 1;

    public Transform ground;
    public Transform player;
    public Transform obstaclePrefab;
    public Transform endGamePrefab;

    void Start()
    {
        transform.position = player.transform.position + Vector3.forward * distanceBetweenRows;
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
        var obstaclesGenerator = new RowsObstaclesStrategy(ground, new RowsObstaclesStrategy.Configuration
        {
            distanceBetweenRows = distanceBetweenRows,
            rows = rows,
            maxGapSize = maxGapSize,
            minGapSize = minGapSize,
            minimumObstacleWidth = minimumObstacleWidth,
        });

        var obstacles = obstaclesGenerator.Generate(transform.position);
        foreach (var obstacle in obstacles)
            CreateObstacle(obstacle);
        var lastObstaclePosition = obstacles.Max(o => o.Position.z) * Vector3.forward;
        CreateEndGame(lastObstaclePosition);
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
