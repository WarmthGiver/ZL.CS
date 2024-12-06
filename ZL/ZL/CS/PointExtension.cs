using System.Drawing;

namespace ZL.CS
{
    public static class PointExtension
    {
        public static Point Add(this Point instance, Point value)
        {
            return new Point(instance.X + value.X, instance.Y + value.Y);
        }

        public static Point Sub(this Point instance, Point value)
        {
            return new Point(instance.X - value.X, instance.Y - value.Y);
        }
    }
}