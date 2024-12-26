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
    }
}