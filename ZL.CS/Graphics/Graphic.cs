using System.Drawing;

namespace ZL.CS.Graphics
{
    public abstract class Graphic
    {
        public readonly Rectangle rect;

        public readonly Point pivot;

        public readonly byte[,]? colorMap = null;

        protected Graphic(byte[,] colorMap) : this(colorMap.GetSize())
        {
            this.colorMap = colorMap;
        }

        protected Graphic(Size size)
        {
            rect = new Rectangle(new Point(0, 0), size);

            pivot = size.GetPivot();
        }

        public abstract void Draw(Canvas canvas, Point point, byte depth);
    }
}