using UnityEngine;

namespace TurtleGamesStudio
{
    [CreateAssetMenu(fileName = nameof(Models), menuName = nameof(Models))]
    public class Models : ScriptableObject
    {
        [field: SerializeField] public WallView WallView { get; private set; }
        [field: SerializeField] public FloorView FloorView { get; private set; }
        [field: SerializeField] public SnakeView SnakeView { get; private set; }
        [field: SerializeField] public TailView TailView { get; private set; }
        [field: SerializeField] public FoodView FoodView { get; private set; }
    }
}
