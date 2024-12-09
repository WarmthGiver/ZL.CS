﻿using System.Drawing;

namespace ZL.CS
{
    public static class SizeExtension
    {
        public static Point GetPivot(this Size instance)
        {
            return new(instance.Width >> 1, instance.Height >> 1);
        }

        public static Rectangle ToRect(this Size instance)
        {
            return new(new(), instance);
        }
    }
}