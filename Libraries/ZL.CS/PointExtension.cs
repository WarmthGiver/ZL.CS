using System.Drawing;

namespace ZL.CS
{
    public static partial class PointExtension
    {
        public static Point Add(this Point instance, Point value)
        {
            return new Point(instance.X + value.X, instance.Y + value.Y);
        }

        public static Point Sub(this Point instance, Point value)
        {
            return new Point(instance.X - value.X, instance.Y - value.Y);
        }

        public static Size Direction(this Point instance, Point value)
        {
            return new Size(instance.X - value.X, instance.Y - value.Y);
        }
    }
}