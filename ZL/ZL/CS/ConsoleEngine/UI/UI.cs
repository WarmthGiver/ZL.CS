using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine.UI
{
    public abstract class UI<TGraphic> : Component

        where TGraphic : Graphic
    {
        public TGraphic? graphic = null;

        protected UI(SceneObject sceneObject) : base(sceneObject) { }

        protected override void Update()
        {
            graphic?.Draw(sceneObject.canvas, sceneObject.position, sceneObject.depth);
        }
    }
}