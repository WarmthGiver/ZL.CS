﻿using System.Drawing;

namespace ZL.CS
{
    public static partial class RectangleExtension
    {
        public static Rectangle Scaling(this Rectangle instance, int x, int y)
        {
            instance.X += x;

            instance.Y += y;

            instance.Width -= x << 1;

            instance.Height -= y << 1;

            return instance;
        }
    }
}