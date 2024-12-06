using System;
using System.Drawing;

namespace ZL.CS
{
    public static class SizeExtension
    {
        public static Point GetPivot(this Size instance)
        {
            return new(instance.Width >> 1, instance.Height >> 1);
        }

        /*
        public static Size Clamp(this Size instance, out Size offset)
        {
            offset = new(0, 0);

            if (instance.Width < 0)
            {
                offset.Width = -instance.Width;

                instance.Width = 0;
            }

            if (instance.Height > max.Height)
            {
                offset.Height = -instance.Height;
            }

            if (instance.Height < 0)
            {
                offset.Height = -instance.Height;

                instance.Height = 0;
            }

            instance.Width = Math.Clamp(instance.Width, 0, max.Width);
            instance.Height = Math.Clamp(instance.Height, 0, max.Height);



            return instance;
        }

        public static Size Clamp(this Size instance, Size max, out Size offset)
        {
            offset = new(0, 0);

            if (instance.Width < 0)
            {
                offset.Width = -instance.Width;

                instance.Width = 0;
            }

            if (instance.Height > max.Height)
            {
                offset.Height = -instance.Height;
            }

            if (instance.Height < 0)
            {
                offset.Height = -instance.Height;

                instance.Height = 0;
            }

            instance.Width = Math.Clamp(instance.Width, 0, max.Width);
            instance.Height = Math.Clamp(instance.Height, 0, max.Height);



            return instance;
        }
        */
    }
}