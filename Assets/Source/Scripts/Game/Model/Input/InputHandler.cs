using System;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class InputHandler
    {
        private readonly SnakeClearInput _input;
        private Snake _ownSnake;

        public bool HasSnake { get; private set; } = false;

        public InputHandler(SnakeClearInput input)
        {
            _input = input;
        }

        public void SetSnake(Snake ownSnake)
        {
            if (HasSnake)
                throw new InvalidOperationException("You already have snake");

            HasSnake = true;
            _ownSnake = ownSnake;
        }

        public void RemoveSnake()
        {
            if (!HasSnake)
                throw new InvalidOperationException("You don't have snake");

            HasSnake = false;
            _ownSnake = null;
        }

        public void Tick(Vector2 direction, bool isAccelerationButtonDown)
        {
            if (HasSnake)
            {
                _input.Angle = Vector2.SignedAngle(_ownSnake.transform.up, direction);
                _input.IsAccelerationPressed = isAccelerationButtonDown;
            }
        }
    }
}