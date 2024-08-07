using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TurtleGamesStudio
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField, Min(0)] private float _handleRange = 1;
        [SerializeField, Min(0)] private float _deadZone = 0;
        [SerializeField] private AxisOptions _axisOptions = AxisOptions.Both;
        [SerializeField] private bool _snapX = false;
        [SerializeField] private bool _snapY = false;

        [SerializeField] private RectTransform _background = null;
        [SerializeField] private RectTransform _handle = null;

        private float _sqrDeadZone;
        private Canvas _canvas;
        private Camera _camera;
        private Vector2 _radius;

        private Vector2 _input = Vector2.zero;

        public event Action Pressed;
        public event Action Released;
        public event Action Dragged;

        public float Horizontal { get { return (_snapX) ? SnapFloat(_input.x, AxisOptions.Horizontal) : _input.x; } }
        public float Vertical { get { return (_snapY) ? SnapFloat(_input.y, AxisOptions.Vertical) : _input.y; } }
        public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }
        public float Share { get; private set; } = 0f;

        public float HandleRange
        {
            get { return _handleRange; }
            set { _handleRange = Mathf.Abs(value); }
        }

        public float DeadZone
        {
            get { return _deadZone; }
            set { _deadZone = Mathf.Abs(value); }
        }

        public AxisOptions AxisOptions { get { return AxisOptions; } set { _axisOptions = value; } }
        public bool SnapX { get { return _snapX; } set { _snapX = value; } }
        public bool SnapY { get { return _snapY; } set { _snapY = value; } }

        public void Init(Camera camera)
        {
            _camera = camera;
            HandleRange = _handleRange;
            DeadZone = _deadZone;
            _sqrDeadZone = _deadZone * _deadZone;
            _canvas = GetComponentInParent<Canvas>();

            if (_canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            _radius = _background.sizeDelta / 2;
            Vector2 center = new Vector2(0.5f, 0.5f);
            _background.pivot = center;
            _handle.anchorMin = center;
            _handle.anchorMax = center;
            _handle.pivot = center;
            _handle.anchoredPosition = Vector2.zero;
        }

        private void OnDragHandler(PointerEventData eventData)
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(_camera, _background.position);

            _input = (eventData.position - position) / (_radius * _canvas.scaleFactor);
            FormatInput();
            HandleInput(_input.sqrMagnitude, _input.normalized);
            _handle.anchoredPosition = _input * _radius * _handleRange;

            Share = _input.magnitude;
        }

        private void HandleInput(float sqrMagnitude, Vector2 normalised)
        {
            if (sqrMagnitude > _sqrDeadZone)
            {
                if (sqrMagnitude > 1)
                    _input = normalised;
            }
            else
                _input = Vector2.zero;
        }

        private void FormatInput()
        {
            if (_axisOptions == AxisOptions.Horizontal)
                _input = new Vector2(_input.x, 0f);
            else if (_axisOptions == AxisOptions.Vertical)
                _input = new Vector2(0f, _input.y);
        }

        private float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
                return value;

            if (_axisOptions == AxisOptions.Both)
            {
                float angle = Vector2.Angle(_input, Vector2.up);
                if (snapAxis == AxisOptions.Horizontal)
                {
                    if (angle < 22.5f || angle > 157.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }
                else if (snapAxis == AxisOptions.Vertical)
                {
                    if (angle > 67.5f && angle < 112.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }
                return value;
            }
            else
            {
                if (value > 0)
                    return 1;
                if (value < 0)
                    return -1;
            }
            return 0;
        }

        private void OnPointerUp()
        {
            _input = Vector2.zero;
            Share = 0f;
            _handle.anchoredPosition = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDragHandler(eventData);
            Pressed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUp();
            Released?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragHandler(eventData);
            Dragged?.Invoke();
        }
    }

    public enum AxisOptions { Both, Horizontal, Vertical }
}
