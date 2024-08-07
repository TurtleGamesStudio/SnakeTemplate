using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    [CreateAssetMenu(fileName = nameof(GrowthSettings), menuName = nameof(GrowthSettings))]
    public class GrowthSettings : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int MinSnakeLength { get; private set; } = 10;
        [field: SerializeField, Min(1)] public int MassRequireForGrowth { get; private set; } = 10;
    }
}
