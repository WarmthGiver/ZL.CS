using System.Drawing;

namespace ZL.CS
{
    public static partial class StringExtensions
    {
        public static char[,] ToChar(this string[] instance)
        {
            Size size = instance.GetMaxSize();

            var charMap = new char[size.Height, size.Width];

            charMap.Fill(' ');

            for (int y = 0; y < size.Height; ++y)
            {
                for (int x = 0; x < size.Width; ++x)
                {
                    if (x < instance[y].Length)
                    {
                        charMap[y, x] = instance[y][x];
                    }

                    else
                    {
                        charMap[y, x] = ' ';
                    }
                }
            }

            return charMap;
        }

        public static Size GetMaxSize(this string[] instance)
        {
            return new(instance.GetMaxWidth(), instance.Length);
        }

        public static int GetMaxWidth(this string[] instance)
        {
            int width = instance[0].Length;

            for (int i = 1; i < instance.Length; ++i)
            {
                if (width < instance[i].Length)
                {
                    width = instance[i].Length;
                }
            }

            return width;
        }
    }
}