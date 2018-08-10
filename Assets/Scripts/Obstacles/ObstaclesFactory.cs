using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts.Obstacles
{
    public class ObstaclesFactory
    {
        private readonly Transform ground;
        private readonly Transform player;

        public ObstaclesFactory(Transform ground, Transform player)
        {
            this.ground = ground;
            this.player = player;
        }

        public IEnumerable<IObstaclesGenerationStrategy> Create(string json)
        {
            JArray config = JsonConvert.DeserializeObject(json) as JArray;
            return config.Select(CreateObstaclesStrategy);
        }

        private IObstaclesGenerationStrategy CreateObstaclesStrategy(JToken segment)
        {
            var type = (ObstacleType)Enum.Parse(typeof(ObstacleType), segment.Value<string>("type"));
            var config = segment.Value<JObject>("config");

            switch (type)
            {
                case ObstacleType.SingleRow:
                    return new RowsObstaclesStrategy(
                        ground,
                        config.ToObject<RowsObstaclesStrategy.SingleRowConfiguration>());
                case ObstacleType.Rows:
                    return new RowsObstaclesStrategy(
                        ground,
                        config.ToObject<RowsObstaclesStrategy.Configuration>());
                case ObstacleType.Snake:
                    return new SnakeCourseObstaclesStrategy(
                        ground, 
                        player, 
                        config.ToObject<SnakeCourseObstaclesStrategy.Configuration>());
                default:
                    throw new Exception("Invalid segment type");
            }
        }
    }
}

