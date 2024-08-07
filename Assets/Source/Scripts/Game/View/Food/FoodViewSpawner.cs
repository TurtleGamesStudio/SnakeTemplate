using Reflex.Core;
using Reflex.Injectors;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class FoodViewSpawner
    {
        private readonly FoodView _foodViewTemplate;
        private readonly IPositionConverter _positionConverter;
        private readonly Container _diContainer;
        private readonly Transform _container;
        private readonly int _order;

        public FoodViewSpawner(FoodView foodView, IPositionConverter positionConverter,
            Container diContainer, Transform container, int order)
        {
            _foodViewTemplate = foodView;
            _positionConverter = positionConverter;
            _diContainer = diContainer;
            _container = container;
            _order = order;
        }

        public void Spawn(Food food)
        {
            Vector3 viewPosition = _positionConverter.ConvertPosition(food.transform.position);
            FoodView foodView = GameObject.Instantiate(_foodViewTemplate, viewPosition, Quaternion.identity, _container);
            AttributeInjector.Inject(foodView, _diContainer);
            foodView.Init(food, _order);
        }
    }
}
