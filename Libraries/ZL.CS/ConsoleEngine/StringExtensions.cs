using System.Collections.Generic;

using System.Drawing;

using ZL.CS.ConsoleEngine;

namespace ZL.CS
{
    public static partial class StringExtension
    {
        public static List<FixedChar>[] ToFixedChar(this string[] instance)
        {
            Size size = instance.GetMaxSize();

            var fixedCharMap = new List<FixedChar>[size.Height];

            for (int y = 0; y < size.Height; ++y)
            {
                fixedCharMap[y] = instance[y].ToFixedChar();
            }

            return fixedCharMap;
        }
    }
}