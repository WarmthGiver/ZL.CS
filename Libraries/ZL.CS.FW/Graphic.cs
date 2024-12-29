using System;

using System.Drawing;

using System.Numerics;

namespace ZL.CS.FW
{
    public abstract class Graphic : IDrawable
    {
        public readonly Size size;

        public readonly SizeF pivot;

        public readonly byte[,]? colorMap;

        protected Graphic(byte[,] colorMap) : this(colorMap.GetSize(), colorMap) { }

        protected Graphic(Size size, byte[,]? colorMap = null)
        {
            this.size = size;

            pivot = size.GetPivot();

            this.colorMap = colorMap;
        }

        public void Draw(Vector3 position)
        {
            Camera.Main?.Draw(this, position);
        }

        public Rectangle GetCulledRect(PointF location, RectangleF rect)
        {
            Rectangle result = size.ToRect();

            if (location.X < rect.X)
            {
                result.X = (int)MathF.Round(rect.X - location.X);
            }

            if (location.Y < rect.Y)
            {
                result.Y = (int)MathF.Round(rect.Y - location.Y);
            }

            if (location.X + result.Width >= rect.Right)
            {
                result.Width = (int)MathF.Round(rect.Right - location.X);
            }

            if (location.Y + result.Height >= rect.Bottom)
            {
                result.Height = (int)MathF.Round(rect.Bottom - location.Y);
            }

            return result;
        }
    }
}