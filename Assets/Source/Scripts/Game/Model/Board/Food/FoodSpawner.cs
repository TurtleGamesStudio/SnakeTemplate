using System;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class FoodSpawner
    {
        private readonly AnimationCurve _radiusMassDependency;
        private readonly Food _foodTemplate;

        public FoodSpawner(Food foodTemplate, FoodSettings foodSettings)
        {
            _foodTemplate = foodTemplate;
            _radiusMassDependency = foodSettings.RadiusMassDependency;
        }

        public event Action<Food> Spawned;

        public Food Spawn(int mass, Vector2 position, Transform container)
        {
            Food food = GameObject.Instantiate(_foodTemplate, position, Quaternion.identity, container);
            float radius = _radiusMassDependency.Evaluate(mass);
            food.Init(mass, radius);

            Spawned?.Invoke(food);

            return food;
        }
    }
}
