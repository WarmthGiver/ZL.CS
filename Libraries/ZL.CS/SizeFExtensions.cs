using System.Drawing;

namespace ZL.CS
{
    public static partial class SizeFExtension
    {
        public static SizeF GetPivot(this SizeF instance)
        {
            return new(instance.Width * 0.5f, instance.Height * 0.5f);
        }

        public static RectangleF ToRect(this SizeF instance)
        {
            return new(new(0, 0), instance);
        }

        public static RectangleF ToRect(this SizeF instance, PointF location)
        {
            return new(location, instance);
        }
    }
}