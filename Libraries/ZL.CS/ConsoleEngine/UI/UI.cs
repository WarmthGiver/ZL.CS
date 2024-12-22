using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine.UI
{
    public abstract class UI<TGraphic> : Component

        where TGraphic : Graphic
    {
        public TGraphic? graphic = null;

        internal override void DrawCall()
        {
            graphic?.DrawCall(ConsoleObject.Transform.Position);
        }
    }
}