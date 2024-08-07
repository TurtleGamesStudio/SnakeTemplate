using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class MobileInput : MonoBehaviour, IInputHandler, IAccelerationController, IDirectionController
    {
        private InputHandler _inputHandler;
        private bool _isAccelerationButtonDown = false;

        public Vector2 Direction { get; set; } = Vector2.up;

        public void Init(SnakeClearInput input)
        {
            _inputHandler = new InputHandler(input);
        }

        private void Update()
        {
            Vector2 direction = Direction;
            bool isAccelerationButtonDown = _isAccelerationButtonDown;

            _inputHandler.Tick(direction, isAccelerationButtonDown);
        }

        public void SetSnake(Snake ownSnake)
        {
            _inputHandler.SetSnake(ownSnake);
        }

        public void RemoveSnake()
        {
            _inputHandler.RemoveSnake();
        }

        public void PressAcceleration()
        {
            _isAccelerationButtonDown = true;
        }

        public void ReleaseAcceleration()
        {
            _isAccelerationButtonDown = false;
        }
    }
}
