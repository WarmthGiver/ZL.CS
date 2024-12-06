using System.Drawing;

namespace ZL.CS
{
    public static class RectangleExtension
    {
        public static Point GetPivot(this Rectangle instance)
        {
            return new Size(instance.Right, instance.Bottom).GetPivot();
        }
    }
}