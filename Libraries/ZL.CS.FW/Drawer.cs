namespace ZL.CS.FW
{
    public abstract class Drawer<T> : Component

        where T : class, IDrawable
    {
        public T? target = null;

        public Drawer() : base()
        {
            container.AddRectTransform();
        }

        internal override void CallDraw()
        {
            target.Draw(container.Transform.Position);
        }
    }
}