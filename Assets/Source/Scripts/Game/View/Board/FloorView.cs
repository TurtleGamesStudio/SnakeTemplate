using UnityEngine;

namespace TurtleGamesStudio
{
    public class FloorView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public void Init(Vector2 scale, int order)
        {
            _meshRenderer.sortingOrder = order;
            SetScale(scale);
        }

        public void SetScale(Vector2 scale)
        {
            _meshRenderer.material.mainTextureScale = scale;
        }
    }
}
