using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    [CreateAssetMenu(fileName = nameof(SeparationCalculator), menuName = nameof(SeparationCalculator))]
    public class SeparationCalculator : ScriptableObject
    {
        [SerializeField] private List<int> _massRequireForSeparation;

        private List<int> _massCountDependency;

        public void Init()
        {
            _massCountDependency = new() { 1 };

            _massCountDependency.AddRange(_massRequireForSeparation);
            _massCountDependency.OrderBy(count => count).Distinct();
        }

        public int GetCount(int mass)
        {
            int count = 0;

            for (int i = 0; i < _massCountDependency.Count; i++)
            {
                if (_massCountDependency[i] > mass)
                    break;

                count++;
            }

            return count;
        }
    }
}
