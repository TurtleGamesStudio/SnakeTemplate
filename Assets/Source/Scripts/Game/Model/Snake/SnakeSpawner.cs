using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class SnakeSpawner : MonoBehaviour
    {
        [SerializeField] private GrowthSettings _growthSettings;
        [SerializeField, Min(0)] private float _previousDirectionKoefficient = 0.5f;
        [SerializeField] private Transform _container;

        private AnimationCurve _thicknessMassDependency;
        private SnakeLengthCalculator _lengthCalculator;

        private Snake _snakeTemplate;

        [Inject]
        private void Inject(Container container)
        {
            _snakeTemplate = container.Single<Snake>();
        }

        public void Init(AnimationCurve thicknessMassDependency)
        {
            _thicknessMassDependency = thicknessMassDependency;
            _lengthCalculator = new SnakeLengthCalculator(_growthSettings.MinSnakeLength,
                _growthSettings.MassRequireForGrowth);
        }

        public Snake Spawn(Vector2 modelPosition, ISnakeInput input)
        {
            Snake snake = Instantiate(_snakeTemplate, modelPosition, Quaternion.identity, _container);
            snake.Init(input, _lengthCalculator, _thicknessMassDependency,
                _previousDirectionKoefficient);
            return snake;
        }
    }
}
