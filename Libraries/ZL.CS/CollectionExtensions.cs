using System.Collections;

using System.Drawing;

using System.Linq;

namespace ZL.CS
{
    public static partial class CollectionExtensions
    {
        public static Size GetMaxSize<T>(this T[] instance)

            where T : ICollection
        {
            return new(instance.GetMaxWidth(), instance.Length);
        }

        public static int GetMaxWidth<T>(this T[] instance)

            where T : ICollection
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

        public static int GetTotalLength<T>(this T[] instance)

            where T : ICollection
        {
            return instance.Sum(s => s.Count);
        }
    }
}