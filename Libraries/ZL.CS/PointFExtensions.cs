using System;

using System.Drawing;

namespace ZL.CS
{
    public static partial class PointFExtension
    {
        public static PointF Add(this PointF instance, PointF value)
        {
            return new(instance.X + value.X, instance.Y + value.Y);
        }

        public static PointF Sub(this PointF instance, PointF value)
        {
            return new(instance.X - value.X, instance.Y - value.Y);
        }

        public static SizeF Direction(this PointF instance, PointF value)
        {
            return new(instance.X - value.X, instance.Y - value.Y);
        }

        public static Point Round(this PointF instance)
        {
            return new((int)MathF.Round(instance.X), (int)MathF.Round(instance.Y));
        }
    }
}