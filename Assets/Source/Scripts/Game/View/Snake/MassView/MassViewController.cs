using TurtleGamesStudio.SnakeGame.Model;

namespace TurtleGamesStudio
{
    public class MassViewController
    {
        private readonly MassView _massView;
        private readonly RebornSystem _rebornSystem;

        public MassViewController(RebornSystem rebornSystem, MassView massView)
        {
            _rebornSystem = rebornSystem;
            _massView = massView;
            _massView.Init(_rebornSystem.Player.Mass);
            _rebornSystem.GameLost += OnLost;
            _rebornSystem.GameRestarted += OnRestarted;
        }

        private void OnLost()
        {
            _massView.gameObject.SetActive(false);
        }

        private void OnRestarted()
        {
            _massView.gameObject.SetActive(true);
            _massView.Init(_rebornSystem.Player.Mass);
        }
    }
}
