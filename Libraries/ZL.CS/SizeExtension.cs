using System.Drawing;

namespace ZL.CS
{
    public static class SizeExtension
    {
        public static Size GetHalf(this Size instance)
        {
            return new(instance.Width >> 1, instance.Height >> 1);
        }

        /*
        public static Point ToPoint(this Size instance)
        {
            return new(instance.Width, instance.Height);
        }
        */

        /*
        public static Point GetPivot(this Size instance)
        {
            return new(instance.Width >> 1, instance.Height >> 1);
        }
        */

        public static Rectangle ToRect(this Size instance)
        {
            return new(new(0, 0), instance);
        }
    }
}