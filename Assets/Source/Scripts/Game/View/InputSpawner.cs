using System;
using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class InputSpawner : MonoBehaviour
    {
        [SerializeField] private DesktopInput _desktopInputTemplate;
        [SerializeField] private MobileInput _mobileInputTemplate;

        public IInputHandler InputHandler { get; private set; }
        public SnakeClearInput ClearInput { get; private set; }

        private Platform _platform;
        private GameMainUi _gameMainUi;

        [Inject]
        private void Inject(Container container)
        {
            _platform = container.Single<Platform>();
            _gameMainUi = container.Single<GameMainUi>();
        }

        public void Spawn()
        {
            ClearInput = new SnakeClearInput();

            switch (_platform)
            {
                case Platform.Desktop:
                    DesktopInput desktopInput = Instantiate(_desktopInputTemplate, transform);
                    desktopInput.Init(ClearInput);
                    InputHandler = desktopInput;
                    break;
                case Platform.Mobile:
                    MobileInput mobileInput = Instantiate(_mobileInputTemplate, transform);
                    mobileInput.Init(ClearInput);
                    InputHandler = mobileInput;
                    _gameMainUi.InitMobileInterface(mobileInput);
                    break;
                default:
                    throw new NotImplementedException($"Input for {_platform} platform not implemented");
            }
        }
    }
}
