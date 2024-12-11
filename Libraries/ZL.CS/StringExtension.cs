using System.Drawing;

namespace ZL.CS
{
    public static class StringExtension
    {
        public static char[,] ToChar(this string[] instance)
        {
            Size size = instance.GetSize();

            var textMap = new char[size.Height, size.Width];

            textMap.Fill(' ');

            for (int y = 0; y < size.Height; ++y)
            {
                for (int x = 0; x < size.Width; ++x)
                {
                    if (x < instance[y].Length)
                    {
                        textMap[y, x] = instance[y][x];
                    }

                    else
                    {
                        textMap[y, x] = ' ';
                    }
                }
            }

            return textMap;
        }

        public static Size GetSize(this string[] instance)
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