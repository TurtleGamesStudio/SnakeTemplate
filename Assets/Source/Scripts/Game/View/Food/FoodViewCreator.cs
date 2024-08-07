using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.ExtendedCollections;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class FoodViewCreator : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private IReadOnlyReactiveList<Food> _allFood;
        private FoodViewSpawner _foodViewSpawner;

        [Inject]
        private void Inject(Container container)
        {
            FoodView foodView = container.Single<FoodView>();
            IPositionConverter positionConverter = container.Single<IPositionConverter>();
            int foodOrder = container.Single<RenderOrder>().Order[RenderName.Food];

            _foodViewSpawner = new FoodViewSpawner(foodView, positionConverter,
                 container, _container, foodOrder);
        }

        public void Init(IReadOnlyReactiveList<Food> allFood)
        {
            _allFood = allFood;

            foreach (var food in _allFood.List)
                Spawn(food);

            _allFood.Added += Spawn;
        }

        private void OnDestroy()
        {
            _allFood.Added -= Spawn;
        }

        public void Spawn(Food food)
        {
            _foodViewSpawner.Spawn(food);
        }
    }
}
