using Cinemachine;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class SnakeCameraController
    {
        private readonly RebornSystem _rebornSystem;

        private readonly Vector3 _baseFollowOffset;
        private readonly CinemachineTransposer _cinemachineTransposer;

        public SnakeCameraController(RebornSystem rebornSystem, Vector3 baseFollowOffset,
            CinemachineVirtualCamera camera)
        {
            _rebornSystem = rebornSystem;
            _baseFollowOffset = baseFollowOffset;
            _cinemachineTransposer = camera.GetCinemachineComponent<CinemachineTransposer>();
            _rebornSystem.GameRestarted += OnRestarted;
            OnRestarted();
        }

        private void OnRestarted()
        {
            new SnakeCamera(_baseFollowOffset, _cinemachineTransposer,
                _rebornSystem.Player.ThicknessAndLengthUpdater);
        }
    }
}
