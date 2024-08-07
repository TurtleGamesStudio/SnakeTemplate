using System;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    [RequireComponent(typeof(SnakeMovement))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Snake : MonoBehaviour
    {
        private CircleCollider2D _circleCollider;
        private SnakeLengthCalculator _lengthCalculator;
        private Mass _mass;
        private SnakeMovement _snakeMovement;
        private TailPoints _tailPoints;
        private ThicknessAndLengthUpdater _thicknessAndLengthUpdater;

        public event Action<Snake> Died;
        public event Action PositionUpdated;

        public Mass Mass => _mass;
        public TailPoints TailPoints => _tailPoints;
        public ThicknessAndLengthUpdater ThicknessAndLengthUpdater => _thicknessAndLengthUpdater;

        public void Init(ISnakeInput input, SnakeLengthCalculator lengthCalculator,
            AnimationCurve thicknessMassDependency, float previousDirectionKoefficient)
        {
            _lengthCalculator = lengthCalculator;
            _circleCollider = GetComponent<CircleCollider2D>();
            _snakeMovement = GetComponent<SnakeMovement>();
            _mass = new Mass(0);
            _thicknessAndLengthUpdater = new ThicknessAndLengthUpdater(_mass,
                thicknessMassDependency, _lengthCalculator);

            _snakeMovement.Init(input, _thicknessAndLengthUpdater);
            _snakeMovement.PositionUpdated += OnPositionUpdated;

            _tailPoints = new TailPoints(_thicknessAndLengthUpdater, this, previousDirectionKoefficient);
            _thicknessAndLengthUpdater.ThicknessChanged += OnThicknessChanged;
            OnThicknessChanged();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Food food))
            {
                AddMass(food.Mass);
                food.Collect();
            }
            else if (collision.TryGetComponent(out Wall wall))
            {
                Die();
            }
            else if (collision.TryGetComponent(out Snake collidedHead))
            {
                Die();
            }
            else if (collision.TryGetComponent(out Tail tail))
            {
                Snake collidedSnake = tail.Owner;

                OnSnakeCollided(collidedSnake);
            }
        }

        private void OnDisable()
        {
            _snakeMovement.PositionUpdated -= OnPositionUpdated;
            _thicknessAndLengthUpdater.ThicknessChanged -= OnThicknessChanged;
        }

        public void Die()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }

        private void AddMass(int mass)
        {
            _mass.AddMass(mass);
        }

        private bool IsOwner(Snake snake)
        {
            return snake == this;
        }

        private void OnPositionUpdated()
        {
            PositionUpdated?.Invoke();
        }

        private void OnSnakeCollided(Snake collidedSnake)
        {
            if (!IsOwner(collidedSnake))
                Die();
        }

        private void OnThicknessChanged()
        {
            _circleCollider.radius = _thicknessAndLengthUpdater.Thickness;
        }
    }
}
