using System;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class SnakeMovement : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _inputAccelerationMultiplier = 2f;
        [SerializeField, Min(0)] private float _baseRotationSpeed = 100f;
        [SerializeField, Min(0)] private float _speed = 100f;

        private ISnakeInput _input;
        private ThicknessAndLengthUpdater _thicknessAndLengthUpdater;
        private float _rotationSpeed;

        public event Action PositionUpdated;

        internal void Init(ISnakeInput input, ThicknessAndLengthUpdater thicknessAndLengthUpdater)
        {
            _input = input;
            _thicknessAndLengthUpdater = thicknessAndLengthUpdater;
            _thicknessAndLengthUpdater.ThicknessChanged += OnThicknessChanged;
            OnThicknessChanged();
        }

        private void OnDisable()
        {
            _thicknessAndLengthUpdater.ThicknessChanged -= OnThicknessChanged;
        }

        private void Update()
        {
            float inputedAccelerationMultiplier = _input.IsAccelerationPressed ?
                _inputAccelerationMultiplier : 1f;

            float sign = Mathf.Sign(_input.Angle);

            float angleInFrame = sign *
                Mathf.Min(sign * _input.Angle, _rotationSpeed * inputedAccelerationMultiplier * Time.deltaTime);

            transform.Rotate(Vector3.forward, angleInFrame, Space.Self);

            transform.localPosition += _speed * inputedAccelerationMultiplier * Time.deltaTime * transform.up;

            PositionUpdated?.Invoke();
        }

        private void OnThicknessChanged()
        {
            _rotationSpeed = _baseRotationSpeed / _thicknessAndLengthUpdater.Thickness;
        }
    }
}
