using System.Drawing;

namespace ZL.CS
{
    public static partial class PointExtensions
    {
        public static Point Add(this Point instance, Point value)
        {
            return new(instance.X + value.X, instance.Y + value.Y);
        }

        public static Point Sub(this Point instance, Point value)
        {
            return new(instance.X - value.X, instance.Y - value.Y);
        }

        public static Size Direction(this Point instance, Point value)
        {
            return new(instance.X - value.X, instance.Y - value.Y);
        }
    }
}