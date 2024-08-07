using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class GameMainUi : MonoBehaviour
    {
        [SerializeField] private MassView _massView;
        [SerializeField] private FinalScreen _finalScreen;

        [Header("MobileInterface")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private AccelerationButton _accelerationButton;
        [SerializeField] private GameObject _mobilePanel;

        private Camera _uiCamera;

        [Inject]
        private void Inject(Container container)
        {
            _uiCamera = container.Single<GameCameras>().MainCamera;
        }

        public void Init(RebornSystem rebornSystem)
        {
            new MassViewController(rebornSystem, _massView);
            _finalScreen.Init(rebornSystem);
        }

        public void InitMobileInterface(MobileInput mobileInput)
        {
            new DirectionJoystick(_joystick, mobileInput);
            _joystick.gameObject.SetActive(true);
            _joystick.Init(_uiCamera);

            _accelerationButton.Init(mobileInput);
            _mobilePanel.SetActive(true);
        }
    }
}
