﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class RowsObstaclesStrategy : IObstaclesGenerationStrategy
    {
        [System.Serializable]
        public class Configuration
        {
            public int rows;
            public float distanceBetweenRows;
            public float minGapSize;
            public float maxGapSize;
            public float minimumObstacleWidth;
        }

        [System.Serializable]
        public class SingleRowConfiguration
        {
            public float gapSize;
            public float minimumObstacleWidth;
        }

        private readonly Transform ground;
        private readonly Configuration config;

        public RowsObstaclesStrategy(Transform ground, SingleRowConfiguration config)
        : this(ground, new Configuration
        {
            rows = 1,
            distanceBetweenRows = 0,
            maxGapSize = config.gapSize,
            minGapSize = config.gapSize,
            minimumObstacleWidth = config.minimumObstacleWidth,
        })
        {
        }

        public RowsObstaclesStrategy(Transform ground, Configuration config)
        {
            this.ground = ground;
            this.config = config;
        }

        public IList<Obstacle> Generate(Vector3 startingPosition)
        {
            var rows = new List<Obstacle>();
            for (int i = 0; i < config.rows; i++)
                rows.AddRange(CreateObstacleRow(startingPosition, i));
            return rows;
        }

        private Vector3 GetInitialPositionForRow(Vector3 startingPosition, int row)
        {
            return Vector3.forward * (startingPosition.z + config.distanceBetweenRows * row);
        }

        private float GapSizeForRow(int row)
        {
            return Mathf.Lerp(config.maxGapSize, config.minGapSize, row / config.rows);
        }

        private IList<Obstacle> CreateObstacleRow(Vector3 startingPosition, int row)
        {
            var initialPosition = GetInitialPositionForRow(startingPosition, row);
            var gapSize = GapSizeForRow(row);
            var gapCenterX = RandomGapCenterX(gapSize);

            return new List<Obstacle>
            {
                CreateLeftObstacle(gapSize, initialPosition, gapCenterX),
                CreateRightObstacle(gapSize, initialPosition, gapCenterX)
            };
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
            return Random.Range(config.minimumObstacleWidth, GroundWidth - gapSize - config.minimumObstacleWidth) +
                (gapSize / 2) - GroundWidth / 2;
        }

        private Obstacle CreateLeftObstacle(float gapSize, Vector3 initialPosition, float gapCenterX)
        {
            var gapLeftX = gapCenterX - gapSize / 2;
            var leftWidth = gapLeftX + GroundWidth / 2;
            var leftCenter = gapLeftX - leftWidth / 2;
            return CreateObstacle(initialPosition, leftCenter, leftWidth);
        }

        private Obstacle CreateRightObstacle(float gapSize, Vector3 initialPosition, float gapCenterX)
        {
            var gapRightX = gapCenterX + gapSize / 2;
            var rightWidth = GroundWidth / 2 - gapRightX;
            var rightCenter = gapRightX + rightWidth / 2;
            return CreateObstacle(initialPosition, rightCenter, rightWidth);
        }

        private Obstacle CreateObstacle(Vector3 initialPosition, float centerX, float scaleX)
        {
            var position = initialPosition + new Vector3(centerX, 0, 0);
            return new Obstacle
            {
                Position = position,
                Width = scaleX,
            };
        }
    }
}
