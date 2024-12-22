using System.Collections.Generic;

namespace ZL.CS
{
    public static class FixedCharExtensions
    {
        public static List<FixedChar> ToFixedChar(this string value)
        {
            List<FixedChar> result = new(value.Length);

            FixedChar fixedChar = new();

            for (int i = 0; i < value.Length; ++i)
            {
                if (fixedChar.TryAppend(value[i]) == false)
                {
                    result.Add(fixedChar);

                    fixedChar = new(value[i]);
                }
            }

            fixedChar.TryAppend(' ');

            result.Add(fixedChar);

            result.TrimExcess();

            return result;
        }
    }
}