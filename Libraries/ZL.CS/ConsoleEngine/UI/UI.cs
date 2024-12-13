using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine.UI
{
    public abstract class UI<TGraphic> : Component

        where TGraphic : Graphic
    {
        public TGraphic? graphic = null;

        protected override void FixedUpdate()
        {
            graphic?.DrawCall(consoleObject.Transform.Position);
        }
    }
}