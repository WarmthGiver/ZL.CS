namespace ZL.CS.API
{
    public abstract class GraphicDrawer<TGraphic> : Component

        where TGraphic : Graphic
    {
        public TGraphic? graphic = null;

        internal override void CallDraw()
        {
            Camera.Main?.Draw(graphic, Container.Transform.Position);
        }
    }
}