using System.Collections.Generic;

namespace ZL.CS
{
    public static partial class ListExtensions
    {
        public static T[,] ToArray<T>(this List<T>[] instance, T @default)
        {
            return ToArray(instance, instance.GetMaxWidth(), @default);
        }

        public static T[,] ToArray<T>(this List<T>[] instance, int width, T @default)
        {
            int height = instance.Length;

            var result = new T[height, width];

            for (int y = 0; y < height; ++y)
            {
                int x = 0;

                for (; x < instance[y].Count; ++x)
                {
                    result[y, x] = instance[y][x];
                }

                for (; x < width; ++x)
                {
                    result[y, x] = @default;
                }
            }

            return result;
        }
    }
}