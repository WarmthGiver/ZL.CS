using System.Collections;

using System.Drawing;

namespace ZL.CS
{
    public static partial class ArrayExtension
    {
        public static T Get<T>(this T[,] instance, Point point)
        {
            return instance[point.Y, point.X];
        }

        public static void Set<T>(this T[,] instance, Point point, T value)
        {
            instance[point.Y, point.X] = value;
        }

        public static Size GetSize<T>(this T[,] instance)
        {
            return new(instance.GetLength(1), instance.GetLength(0));
        }

        public static Size GetMaxSize<TCollection>(this TCollection[] instance)

            where TCollection : ICollection
        {
            return new(instance.GetMaxWidth(), instance.Length);
        }

        public static int GetMaxWidth<TCollection>(this TCollection[] instance)

            where TCollection : ICollection
        {
            int width = instance[0].Count;

            for (int i = 1; i < instance.Length; ++i)
            {
                if (width < instance[i].Count)
                {
                    width = instance[i].Count;
                }
            }

            return width;
        }

        public static Point GetMaxIndex<T>(this T[,] instance)
        {
            return new(instance.GetLength(1) - 1, instance.GetLength(0) - 1);
        }

        public static void Fill<T>(this T[,] instance, T value)
        {
            for (int y = instance.GetLength(0); --y >= 0;)
            {
                for (int x = instance.GetLength(1); --x >= 0;)
                {
                    instance[y, x] = value;
                }
            }
        }

        public static void Fill<T>(this T[,] instance, T value, Rectangle padding)
        {
            for (int y = instance.GetLength(0) - padding.Height; --y >= padding.Y;)
            {
                for (int x = instance.GetLength(1) - padding.Width; --x >= padding.X;)
                {
                    instance[y, x] = value;
                }
            }
        }

        public static T[,] ToMap<T>(this T[] instance)
        {
            var map = new T[1, instance.Length];

            for (int x = 0; x < instance.Length; ++x)
            {
                map[0, x] = instance[x];
            }

            return map;
        }
    }
}