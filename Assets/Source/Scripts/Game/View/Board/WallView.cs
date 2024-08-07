using UnityEngine;

namespace TurtleGamesStudio
{
    public class WallView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public void Init(int order)
        {
            _meshRenderer.sortingOrder = order;
        }
    }
}
