using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.ExtendedCollections;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class DroppingFoodController : MonoBehaviour
    {
        [SerializeField] private Transform _droppingFoodContainer;
        [SerializeField, Min(1)] private int _minimumFood = 10;

        private IReadOnlyReactiveList<Snake> _allSnakes;
        private DroppingFood _droppingFood;

        private SeparationCalculator _separationCalculator;
        private FoodSpawner _foodSpawner;

        [Inject]
        private void Inject(Container container)
        {
            _separationCalculator = container.Single<SeparationCalculator>();
            _foodSpawner = container.Single<FoodSpawner>();
        }

        public void Init(IReadOnlyReactiveList<Snake> allSnakes)
        {
            _allSnakes = allSnakes;

            _droppingFood = new DroppingFood(_foodSpawner, _separationCalculator,
                _droppingFoodContainer, _minimumFood);
            _allSnakes.Removed += OnDied;
        }

        private void OnDestroy()
        {
            _allSnakes.Removed -= OnDied;
        }

        private void OnDied(Snake snake)
        {
            _droppingFood.Drop(snake.TailPoints, snake.Mass.Value);
        }
    }
}
