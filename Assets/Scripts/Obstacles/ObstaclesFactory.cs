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

        /// <summary>
        /// Creates a list of obstacle generation strategies based on configuration from a json file
        /// </summary>
        /// <param name="json">string of a json file containing an "obstacles" field 
        /// which is a list of obstacle segment definitions</param>
        /// <returns>List of obstacle generation strategies</returns>
        public IEnumerable<IObstaclesGenerationStrategy> Create(string json)
        {
            var config = JsonConvert.DeserializeObject(json) as JObject;
            return config.Value<JArray>("obstacles").Select(CreateObstaclesStrategy);
        }

        private IObstaclesGenerationStrategy CreateObstaclesStrategy(JToken segment)
        {
            var type = segment.Value<string>("type").ParseToEnum<ObstacleType>();
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

