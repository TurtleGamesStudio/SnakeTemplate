using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    [CreateAssetMenu(fileName = nameof(FoodSettings), menuName = nameof(FoodSettings))]
    public class FoodSettings : ScriptableObject
    {
        [field: SerializeField] public AnimationCurve RadiusMassDependency { get; private set; }
    }
}
