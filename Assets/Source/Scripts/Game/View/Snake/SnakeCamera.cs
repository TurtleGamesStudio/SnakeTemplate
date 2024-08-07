using Cinemachine;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class SnakeCamera
    {
        private readonly Vector3 _baseFollowOffset;
        private readonly CinemachineTransposer _cinemachineTransposer;
        private readonly ThicknessAndLengthUpdater _thicknessAndLengthUpdater;

        public SnakeCamera(Vector3 baseFollowOffset, CinemachineTransposer cinemachineTransposer,
            ThicknessAndLengthUpdater thicknessAndLengthUpdater)
        {
            _baseFollowOffset = baseFollowOffset;
            _cinemachineTransposer = cinemachineTransposer;
            _thicknessAndLengthUpdater = thicknessAndLengthUpdater;
            _thicknessAndLengthUpdater.ThicknessChanged += OnThicknessChanged;
            OnThicknessChanged();
        }

        private void OnThicknessChanged()
        {
            _cinemachineTransposer.m_FollowOffset = _baseFollowOffset * _thicknessAndLengthUpdater.Thickness;
        }
    }
}
