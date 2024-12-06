using System.Drawing;

namespace ZL.CS.Graphics
{
    public abstract class Graphic
    {
        public readonly Size size;
        public readonly Point pivot;

        public readonly int[,]? colorMap = null;

        protected Graphic(int[,] colorMap) : this(colorMap.GetSize())
        {
            this.colorMap = colorMap;
        }

        protected Graphic(Size size)
        {
            this.size = size;
            pivot = size.GetPivot();
        }

        public abstract void Draw(Canvas canvas, Point point, sbyte depth);
    }
}