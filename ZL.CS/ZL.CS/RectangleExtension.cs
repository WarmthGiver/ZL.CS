using System.Drawing;

namespace ZL.CS
{
    public static class RectangleExtension
    {
        public static Point GetPivot(this Rectangle instance)
        {
            return new Size(instance.Right, instance.Bottom).GetPivot();
        }

        public static Rectangle Culling(this Rectangle instance, Rectangle rect, Point location)
        {
            if (location.X < rect.X)
            {
                instance.X = rect.X - location.X;
            }

            if (location.Y < rect.Y)
            {
                instance.Y = rect.Y - location.Y;
            }

            if (location.X + instance.Width >= rect.Width)
            {
                instance.Width = rect.Width - location.X;
            }

            if (location.Y + instance.Height >= rect.Height)
            {
                instance.Height = rect.Height - location.Y;
            }

            return instance;
        }
    }
}