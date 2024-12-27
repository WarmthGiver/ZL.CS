using System.Collections.Generic;

using System.Drawing;

using ZL.CS.API;

namespace ZL.CS
{
    public static partial class StringExtension
    {
        public static List<CharCell>[] ToCharCellMap(this string[] instance)
        {
            Size size = instance.GetMaxSize();

            var charCellMap = new List<CharCell>[size.Height];

            for (int y = 0; y < size.Height; ++y)
            {
                charCellMap[y] = instance[y].ToCharCells();
            }

            return charCellMap;
        }

        public static List<CharCell> ToCharCells(this string value)
        {
            List<CharCell> charCells = new();

            CharCell charCell = new();

            for (int x = 0; x < value.Length; ++x)
            {
                if (charCell.TryAppend(value[x]) == false)
                {
                    charCells.Add(charCell);

                    charCell = new(value[x]);
                }
            }

            charCell.TryAppend(' ');

            charCells.Add(charCell);

            charCells.TrimExcess();

            return charCells;
        }

        public static List<CharCell> ToCharCells(this string[] value)
        {
            List<CharCell> charCells = new();

            CharCell charCell = new();

            for (int y = 0; y < value.Length; ++y)
            {
                for (int x = 0; x < value[y].Length; ++x)
                {
                    if (charCell.TryAppend(value[y][x]) == false)
                    {
                        charCells.Add(charCell);

                        charCell = new(value[y][x]);
                    }
                }
            }

            charCell.TryAppend(' ');

            charCells.Add(charCell);

            charCells.TrimExcess();

            return charCells;
        }
    }
}