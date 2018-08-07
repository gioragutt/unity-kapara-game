using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    public int distanceToEndFromLastRow = 10;
    public int rows = 5;
    public float distanceBetweenRows = 18;
    public float minGapSize = 3;
    public float maxGapSize = 5;
    public float minimumObstacleWidth = 1;

    public Transform ground;
    public Transform obstaclePrefab;
    public Transform endGamePrefab;

    void Start()
    {
        GenerateObstacles();
    }

    public void GenerateObstacles()
    {
        RemoveObstacles();
        for (int i = 0; i < rows; i++)
            CreateObstacleRow(i);
        CreateEndGame();
    }

    private void CreateEndGame()
    {
        var position = GetInitialPositionForRow(rows - 1) + Vector3.forward * distanceToEndFromLastRow;
        Instantiate(endGamePrefab, position, Quaternion.identity, transform);
    }

    public void RemoveObstaclesFromEditor()
    {
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }

    private void RemoveObstacles()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject != null)
                Destroy(child.gameObject);
        }
    }

    private Vector3 GetInitialPositionForRow(int row)
    {
        return Vector3.forward * (transform.position.z + distanceBetweenRows * row);
    }

    private float GapSizeForRow(int row)
    {
        return Mathf.Lerp(maxGapSize, minGapSize, row / rows);
    }

    private void CreateObstacleRow(int row)
    {
        var initialPosition = GetInitialPositionForRow(row);
        var gapSize = GapSizeForRow(row);
        var gapCenterX = RandomGapCenterX(gapSize);
        CreateLeftObstacle(row, gapSize, initialPosition, gapCenterX);
        CreateRightObstacle(row, gapSize, initialPosition, gapCenterX);
    }

    private float GroundWidth
    {
        get
        {
            return ground.transform.localScale.x;
        }
    }

    private float RandomGapCenterX(float gapSize)
    {
        return Random.Range(minimumObstacleWidth, GroundWidth - gapSize - minimumObstacleWidth) +
            (gapSize / 2) - GroundWidth / 2;
    }


    private void CreateLeftObstacle(int row, float gapSize, Vector3 initialPosition, float gapCenterX)
    {
        var gapLeftX = gapCenterX - gapSize / 2;
        var leftWidth = gapLeftX + GroundWidth / 2;
        var leftCenter = gapLeftX - leftWidth / 2;
        CreateObstacle(row, "Left", initialPosition, leftCenter, leftWidth);
    }

    private void CreateRightObstacle(int row, float gapSize, Vector3 initialPosition, float gapCenterX)
    {
        var gapRightX = gapCenterX + gapSize / 2;
        var rightWidth = GroundWidth / 2 - gapRightX;
        var rightCenter = gapRightX + rightWidth / 2;
        CreateObstacle(row, "Right", initialPosition, rightCenter, rightWidth);
    }

    private void CreateObstacle(int row, string side, Vector3 initialPosition, float centerX, float scaleX)
    {
        var position = initialPosition + new Vector3(centerX, 0, 0);
        var obstacle = Instantiate(
            obstaclePrefab,
            position,
            Quaternion.identity,
            transform);

        obstacle.localScale = Vector3.Scale(obstacle.localScale, new Vector3(scaleX, 1, 1));
        obstacle.name = System.String.Format("Row {0}: {1} Obstacle", row, side);
    }
}
