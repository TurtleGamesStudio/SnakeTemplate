using System.Collections.Generic;
using TMPro;
using TurtleGamesStudio.Extensions;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;
using UnityEngine.UI;

namespace TurtleGamesStudio
{
    public class FinalScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restart;
        [SerializeField] private TMP_Text _foodValue;

        private RebornSystem _rebornSystem;

        public void Init(RebornSystem rebornSystem)
        {
            _rebornSystem = rebornSystem;
            _restart.Add(OnRestartButtonClick);
            _rebornSystem.GameLost += Show;
            _rebornSystem.GameRestarted += Hide;
        }

        private void OnDestroy()
        {
            _restart.Remove(OnRestartButtonClick);
            _rebornSystem.GameLost -= Show;
            _rebornSystem.GameRestarted -= Hide;
        }

        private void Show()
        {
            _foodValue.text = _rebornSystem.Player.Mass.Value.ToString() + " kg";
            _panel.SetActive(true);
        }

        private void Hide()
        {
            _panel.SetActive(false);
        }

        private void OnRestartButtonClick()
        {
            _rebornSystem.RespawnPlayer();
        }
    }
}
