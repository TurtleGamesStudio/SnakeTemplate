using UnityEngine;

namespace TurtleGamesStudio
{
    [CreateAssetMenu(fileName = "AnimationCurve", menuName = MenuLiterals.Other + "/AnimationCurve")]
    public class AnimationCurveSO : ScriptableObject
    {
        [field: SerializeField] public AnimationCurve AnimationCurve { get; private set; }
    }
}
