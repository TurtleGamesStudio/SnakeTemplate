using UnityEngine;
using UnityEngine.UI;

namespace TurtleGamesStudio.Utilities
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RaycastTarget : Graphic
    {
        protected override void Awake()
        {
            base.Awake();
            color = new Color(1, 1, 1, 0);
        }
        public override void SetMaterialDirty() { return; }
        public override void SetVerticesDirty() { return; }
    }
}