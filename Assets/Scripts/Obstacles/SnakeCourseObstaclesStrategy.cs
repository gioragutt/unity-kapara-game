using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class SnakeCourseObstaclesStrategy : IObstaclesGenerationStrategy
    {
        [System.Serializable]
        public class Configuration
        {
            public int rows;
            public float width;
            public float distanceBetweenRows;
        }

        private readonly Configuration config;
        private readonly Transform connectingObstacle;
        private readonly RowsObstaclesStrategy rowsGenerator;

        public SnakeCourseObstaclesStrategy(Transform ground, Transform connectingObstacle, Configuration config)
        {
            this.config = config;
            this.connectingObstacle = connectingObstacle;

            rowsGenerator = new RowsObstaclesStrategy(ground, new RowsObstaclesStrategy.Configuration
            {
                rows = config.rows,
                distanceBetweenRows = config.distanceBetweenRows,
                maxGapSize = config.width,
                minGapSize = config.width,
                minimumObstacleWidth = 1,
            });
        }

        public IList<Obstacle> Generate(Vector3 startingPosition)
        {
            var obstacles = new List<Obstacle>();
            var rows = rowsGenerator.Generate(startingPosition);
            obstacles.AddRange(rows);
            for (int i = 0; i < rows.Count - 2; i += 2)
            {
                var opening = GapForRow(rows[i], rows[i + 1]);
                var closing = GapForRow(rows[i + 2], rows[i + 3]);
                obstacles.AddRange(ConnectRowGaps(opening, closing));
            }
            return obstacles;
        }

        private Obstacle GapForRow(Obstacle left, Obstacle right)
        {
            var rightOfLeft = left.Position.x + left.Width / 2;
            var leftOfRight = right.Position.x - right.Width / 2;
            var gapWidth = leftOfRight - rightOfLeft;
            var position = right.Position + Vector3.left * (right.Width / 2 + gapWidth / 2);

            return new Obstacle
            {
                Position = position,
                Width = gapWidth,
            };
        }

        private IList<Obstacle> ConnectRowGaps(Obstacle opening, Obstacle closing)
        {
            var connectingObstacles = new List<Obstacle>();

            for (float z = opening.Position.z + 2; z < closing.Position.z; z += 2)
            {
                var percentToClosing = (z - opening.Position.z) / (closing.Position.z - opening.Position.z);
                var center = Vector3.Lerp(opening.Position, closing.Position, percentToClosing);
                var connectingObstacleWidth = connectingObstacle.localScale.x;
                var distanceFromCenter = config.width / 2 + 0.5f * connectingObstacleWidth;
                var leftPosition = center + Vector3.left * distanceFromCenter;
                var rightPosition = center - Vector3.left * distanceFromCenter;
                var leftObstacle = new Obstacle
                {
                    Position = leftPosition,
                    Width = connectingObstacleWidth,
                };
                var rightObstacle = new Obstacle
                {
                    Position = rightPosition,
                    Width = connectingObstacleWidth,
                };
                connectingObstacles.Add(leftObstacle);
                connectingObstacles.Add(rightObstacle);
            }

            return connectingObstacles;
        }
    }
}
