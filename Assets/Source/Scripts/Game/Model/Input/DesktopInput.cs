using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class DesktopInput : MonoBehaviour, IInputHandler
    {
        private Vector2 _screenCenter;
        private InputHandler _inputHandler;

        public bool HasSnake { get; private set; } = false;

        public void Init(SnakeClearInput input)
        {
            _inputHandler = new InputHandler(input);
            _screenCenter = new Vector2(Screen.width, Screen.height) / 2f;
        }

        private void Update()
        {
            Vector2 direction = (Vector2)Input.mousePosition - _screenCenter;
            bool isAccelerationButtonDown = Input.GetMouseButton(0);

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
    }
}
