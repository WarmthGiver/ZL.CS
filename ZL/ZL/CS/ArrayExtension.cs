using System.Collections.Generic;
using System.Drawing;

namespace ZL.CS
{
    public static class ArrayExtension
    {
        public static T Get<T>(this T[,] instance, Point point)
        {
            return instance[point.Y, point.X];
        }

        public static void Set<T>(this T[,] instance, Point point, T value)
        {
            instance[point.Y, point.X] = value;
        }

        public static void Fill<T>(this T[,] instance, T value)
        {
            for (int y = instance.GetLength(0); --y > 0;)
            {
                for (int x = instance.GetLength(1); --x > 0;)
                {
                    instance[y, x] = value;
                }
            }
        }

        public static Size GetSize<T>(this T[,] instance)
        {
            return new(instance.GetLength(0), instance.GetLength(1));
        }
    }
}