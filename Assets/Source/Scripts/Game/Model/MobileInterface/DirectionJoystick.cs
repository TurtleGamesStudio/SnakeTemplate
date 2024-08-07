namespace TurtleGamesStudio.SnakeGame.Model
{
    public class DirectionJoystick
    {
        private readonly Joystick _joystick;
        private readonly IDirectionController _controller;

        public DirectionJoystick(Joystick joystick, IDirectionController controller)
        {
            _joystick = joystick;
            _controller = controller;

            _joystick.Dragged += OnDragged;
            _joystick.Pressed += OnPressed;
            _joystick.Released += OnReleased;
        }

        private void OnPressed()
        {
            _controller.Direction = _joystick.Direction;
        }

        private void OnReleased()
        {
            _controller.Direction = _joystick.Direction;
        }

        private void OnDragged()
        {
            _controller.Direction = _joystick.Direction;
        }
    }
}
