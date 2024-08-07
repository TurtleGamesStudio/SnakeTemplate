using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class DroppingFood
    {
        private readonly FoodSpawner _foodSpawner;
        private readonly SeparationCalculator _separationCalculator;
        private readonly Transform _container;
        private readonly int _minimumFood;

        private TailPoints _tailPoints;
        private ThicknessAndLengthUpdater _thicknessAndLengthUpdater;

        public DroppingFood(FoodSpawner foodSpawner, SeparationCalculator separationCalculator,
            Transform container, int minimumFood)
        {
            _foodSpawner = foodSpawner;
            _separationCalculator = separationCalculator;
            _container = container;
            _minimumFood = minimumFood;
        }

        public void Drop(TailPoints tailPoints, long mass)
        {
            _tailPoints = tailPoints;
            _thicknessAndLengthUpdater = _tailPoints.ThicknessAndLengthUpdater;
            long droppedMass = (mass + _minimumFood) / 2;

            float massPerSegment = (float)droppedMass / _thicknessAndLengthUpdater.Length;

            float segmentMass = 0;

            for (int i = 0; i < _thicknessAndLengthUpdater.Length; i++)
            {
                segmentMass += massPerSegment;
                Spawn(i, ref segmentMass);
            }
        }

        private void Spawn(int segmentIndex, ref float remainMass)
        {
            int mass = (int)remainMass;
            remainMass -= mass;

            int count = _separationCalculator.GetCount(mass);
            float massPerPoint = (float)mass / count;

            float pointMass = 0;

            for (int i = 0; i < count; i++)
            {
                pointMass += massPerPoint;
                SpawnPoint(segmentIndex, ref pointMass);
            }
        }

        private void SpawnPoint(int segmentIndex, ref float remainMass)
        {
            int mass = (int)remainMass;
            remainMass -= mass;

            if (mass > 0)
            {
                Vector2 position = GetPosition(_tailPoints.Points[segmentIndex]);
                _foodSpawner.Spawn(mass, position, _container);
            }
        }

        private Vector2 GetPosition(Vector2 center)
        {
            Vector2 offset = Random.insideUnitCircle * _thicknessAndLengthUpdater.Thickness;
            return center + offset;
        }
    }
}
