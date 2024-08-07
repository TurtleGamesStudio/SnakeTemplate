using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class BotDecisioner : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _baseReachedRadius = 0.2f;
        [SerializeField, Min(0)] private float _foodIgnoreRadius = 3f;
        [SerializeField, Min(0)] private float _baseFoodDesicionerRadius = 10f;

        private SnakeClearInput _input;
        private Snake _ownSnake;
        private Board _board;

        private ThicknessAndLengthUpdater _thicknessAndLengthUpdater;
        private float _reachedRadius;
        private float _foodDecisionerRadius;
        private float _sqrReachedRadius;
        private float _sqrFoodIgnoreRadius;
        private bool _isPointReached;
        private Vector2 _target;

        public void Init(SnakeClearInput input, Snake ownSnake, Board board)
        {
            _input = input;
            _ownSnake = ownSnake;
            _thicknessAndLengthUpdater = _ownSnake.ThicknessAndLengthUpdater;
            _board = board;
            OnThicknessChanged();
            GetTarget();
            _ownSnake.Died += OnDie;
            _thicknessAndLengthUpdater.ThicknessChanged += OnThicknessChanged;
        }

        private void OnDisable()
        {
            _ownSnake.Died -= OnDie;
            _thicknessAndLengthUpdater.ThicknessChanged -= OnThicknessChanged;
        }

        private void Update()
        {
            if (_isPointReached)
            {
                GetTarget();
                _isPointReached = false;
            }
            else
            {
                _isPointReached = IsPointReached(_target);
            }

            Vector2 targetDirection = _target - (Vector2)_ownSnake.transform.position;
            _input.Angle = Vector2.SignedAngle(_ownSnake.transform.up, targetDirection);

            _input.IsAccelerationPressed = false;
        }

        private void GetTarget()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_ownSnake.transform.position, _foodDecisionerRadius);

            if (!TryGetTerget(colliders, ref _target))
                _target = _board.Center;
        }

        private bool TryGetTerget(Collider2D[] colliders, ref Vector2 target)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Food food) &&
                       !IsClose(collider.transform.position, _sqrFoodIgnoreRadius))
                {
                    target = collider.transform.position;
                    return true;
                }
            }

            return false;
        }

        private bool IsClose(Vector2 position, float sqrMagnitude)
        {
            return ((Vector2)_ownSnake.transform.position - position).sqrMagnitude < sqrMagnitude;
        }

        private bool IsPointReached(Vector2 point)
        {
            return ((Vector2)_ownSnake.transform.position - point).sqrMagnitude < _sqrReachedRadius;
        }

        private void OnThicknessChanged()
        {
            _reachedRadius = _baseReachedRadius * _thicknessAndLengthUpdater.Thickness;
            _sqrReachedRadius = _reachedRadius * _reachedRadius;
            float foodIgnoreRadius = _foodIgnoreRadius * _thicknessAndLengthUpdater.Thickness;
            _sqrFoodIgnoreRadius = foodIgnoreRadius * foodIgnoreRadius;
            _foodDecisionerRadius = _baseFoodDesicionerRadius * _thicknessAndLengthUpdater.Thickness;
        }

        private void OnDie(Snake _)
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_target, _reachedRadius);
        }
    }
}
