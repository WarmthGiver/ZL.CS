using System.Collections.Generic;

namespace ZL.CS
{
    public struct FixedChar
    {
        public char left = ' ';

        public char right = ' ';

        public FixedChar(char value)
        {
            left = value;

            right = '\0';
        }

        public bool TryAppend(char value)
        {
            if (value.IsHalfWidth() == true)
            {
                if (left == ' ')
                {
                    left = value;

                    return true;
                }

                else if (right == ' ')
                {
                    right = value;

                    return true;
                }
            }

            else if (left != ' ')
            {
                left = value;

                right = '\0';

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{left}{right}";
        }

        public static List<FixedChar> Convert(string value)
        {
            List<FixedChar> fixedChars = new();

            FixedChar fixedChar = new();

            for (int i = 0; i < value.Length; ++i)
            {
                if (fixedChar.TryAppend(value[i]) == false)
                {
                    fixedChar = new(value[i]);

                    fixedChars.Add(fixedChar);
                }
            }

            return fixedChars;
        }
    }
}