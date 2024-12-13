using System;
using System.Drawing;

namespace ZL.CS.Graphics
{
    public abstract class Graphic
    {
        public readonly Size size;

        public readonly Size pivot;

        public readonly byte[,]? colorMap = null;

        protected Graphic(byte[,] colorMap) : this(colorMap.GetSize())
        {
            this.colorMap = colorMap;
        }

        protected Graphic(Size size)
        {
            this.size = size;

            pivot = size.GetHalf();
        }

        public abstract void DrawCall(Position position);

        public Rectangle GetRect(Point location, Rectangle view)
        {
            var rect = size.ToRect();

            if (location.X < view.X)
            {
                rect.X = view.X - location.X;
            }

            if (location.Y < view.Y)
            {
                rect.Y = view.Y - location.Y;
            }

            if (location.X + rect.Width >= view.Right)
            {
                rect.Width = view.Right - location.X;
            }

            if (location.Y + rect.Height >= view.Bottom)
            {
                rect.Height = view.Bottom - location.Y;
            }

            return rect;
        }
    }
}