using Assets.Scripts.Obstacles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
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

        private IEnumerable<Obstacle> GenerateObstacles(out Vector3 lastObstaclePosition)
        {
            lastObstaclePosition = transform.position;
            var isFirstGenerator = true;

            var allObstacles = new List<Obstacle>();
            foreach (var generator in CreateObstacleGenerators())
            {
                var startingPosition = lastObstaclePosition;
                if (isFirstGenerator)
                    isFirstGenerator = false;
                else
                    startingPosition = startingPosition + Vector3.forward * distanceBetweenObstacleSegments;

                var obstacles = generator.Generate(startingPosition);
                allObstacles.AddRange(obstacles);
                lastObstaclePosition = obstacles.Max(o => o.Position.z) * Vector3.forward;
            }
            return allObstacles;
        }

        private IEnumerable<IObstaclesGenerationStrategy> CreateObstacleGenerators()
        {
            return new ObstaclesFactory(ground, player).Create(obstaclesDefinition.text);
        }

        private Vector3 CreateAllObstacles()
        {
            Vector3 lastObstaclePosition;
            foreach (var obstacle in GenerateObstacles(out lastObstaclePosition))
                CreateObstacle(obstacle);
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
}