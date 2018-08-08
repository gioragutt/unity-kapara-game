using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IObstaclesGenerationStrategy
    {
        IList<Obstacle> Generate(Vector3 startingPosition);
    }
}
