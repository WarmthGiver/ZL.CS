using System.Drawing;

using System.Numerics;

namespace ZL.CS
{
    public static class Vector3Extansions
    {
        public static Point ToPoint(this Vector3 instance)
        {
            return new((int)instance.X, (int)instance.Y);
        }

        public static PointF ToPointF(this Vector3 instance)
        {
            return new(instance.X, instance.Y);
        }

        public static Size ToSize(this Vector3 instance)
        {
            return new((int)instance.X, (int)instance.Y);
        }

        public static SizeF ToSizeF(this Vector3 instance)
        {
            return new(instance.X, instance.Y);
        }
    }
}