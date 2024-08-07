using TMPro;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class MassView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Mass _mass;

        public void Init(Mass mass)
        {
            _mass = mass;
            _mass.MassChanged += OnChanged;
            OnChanged();
        }

        private void OnDisable()
        {
            _mass.MassChanged -= OnChanged;
        }

        private void OnChanged()
        {
            _text.text = _mass.Value.ToString();
        }
    }
}
