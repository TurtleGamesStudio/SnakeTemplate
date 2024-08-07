using System.Collections.Generic;
using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class FoodController : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _maxCount = 2;
        [SerializeField, Min(0)] private float _respawnDelay = 30f;
        [SerializeField, Min(1)] private int _initialMass = 1;

        private readonly List<Food> _spawnedFood = new List<Food>();
        private Vector2 _cellHalfSize;

        private FoodSpawner _foodSpawner;

        [Inject]
        private void Inject(Container container)
        {
            _foodSpawner = container.Single<FoodSpawner>();
        }

        public void Init(Vector2 cellHalfSize)
        {
            _cellHalfSize = cellHalfSize;

            for (int i = 0; i < _maxCount; i++)
                Spawn();
        }

        private void OnDisable()
        {
            foreach (var food in _spawnedFood)
                food.Collected -= OnCollected;
        }

        private void OnCollected(Food food)
        {
            food.Collected -= OnCollected;
            _spawnedFood.Remove(food);
            Invoke(nameof(Respawn), _respawnDelay);
        }

        private void Respawn()
        {
            int count = _maxCount - _spawnedFood.Count;

            for (int i = 0; i < count; i++)
                Spawn();
        }

        private void Spawn()
        {
            float positionX = Random.Range(-_cellHalfSize.x, _cellHalfSize.x);
            float positionY = Random.Range(-_cellHalfSize.y, _cellHalfSize.y);
            Vector2 position = (Vector2)transform.position + new Vector2(positionX, positionY);

            Food food = _foodSpawner.Spawn(_initialMass, position, transform);
            _spawnedFood.Add(food);
            food.Collected += OnCollected;
        }
    }
}
