using UnityEngine;
using UnityEngine.EventSystems;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class AccelerationButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private IAccelerationController _accelerationController;

        public void Init(IAccelerationController accelerationController)
        {
            _accelerationController = accelerationController;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _accelerationController.PressAcceleration();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _accelerationController.ReleaseAcceleration();
        }
    }
}
