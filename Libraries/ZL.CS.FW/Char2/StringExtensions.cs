using System.Collections.Generic;

using ZL.CS.FW;

namespace ZL.CS
{
    public static partial class StringExtension
    {
        public static Char2[,] ToChar2Map(this string[] instance)
        {
            int height = instance.Length;

            int width = 0;

            var char2Lists = new List<Char2>[height];

            for (int y = 0; y < height; ++y)
            {
                char2Lists[y] = instance[y].ToChar2List();

                if (width < char2Lists[y].Count)
                {
                    width = char2Lists[y].Count;
                }
            }

            return char2Lists.ToArray(width, Char2.Blank);
        }

        public static Char2[,] ToChar2Map(this string[] instance, int width)
        {
            int height = instance.Length;

            var char2Map = new Char2[height, width];

            for (int y = 0; y < height; ++y)
            {
                var char2List = instance[y].ToChar2List();

                int x = 0;

                for (; x < char2List.Count; ++x)
                {
                    char2Map[y, x] = char2List[x];
                }

                for (; x < width; ++x)
                {
                    char2Map[y, x] = Char2.Blank;
                }
            }

            return char2Map;
        }

        public static List<Char2> ToChar2List(this string value)
        {
            List<Char2> char2List = new();

            Char2 char2 = new();

            for (int x = 0; x < value.Length; ++x)
            {
                if (char2.TryAppend(value[x]) == false)
                {
                    char2List.Add(char2);

                    char2 = new(value[x]);
                }
            }

            char2.TryAppend(' ');

            char2List.Add(char2);

            char2List.TrimExcess();

            return char2List;
        }

        public static List<Char2> ToChar2List(this string[] value)
        {
            List<Char2> char2List = new();

            Char2 char2 = new();

            for (int y = 0; y < value.Length; ++y)
            {
                for (int x = 0; x < value[y].Length; ++x)
                {
                    if (char2.TryAppend(value[y][x]) == false)
                    {
                        char2List.Add(char2);

                        char2 = new(value[y][x]);
                    }
                }
            }

            char2.TryAppend(' ');

            char2List.Add(char2);

            char2List.TrimExcess();

            return char2List;
        }
    }
}