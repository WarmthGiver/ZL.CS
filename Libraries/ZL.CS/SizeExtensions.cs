using System.Drawing;

namespace ZL.CS
{
    public static partial class SizeExtensions
    {
        public static Size GetPivot(this Size instance)
        {
            return new(instance.Width >> 1, instance.Height >> 1);
        }

        public static Rectangle ToRect(this Size instance)
        {
            return new(new(0, 0), instance);
        }

        public static Rectangle ToRect(this Size instance, Point location)
        {
            return new(location, instance);
        }
    }
}