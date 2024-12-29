namespace ZL.CS.FW
{
    public struct Char2
    {
        public char? Left { get; private set; } = null;

        public char? Right { get; private set; } = null;

        public static readonly Char2 Blank = new() { Left = ' ', Right = ' ' };

        public Char2(char left)
        {
            TryAppend(left);
        }

        public Char2(char left, char right)
        {
            TryAppend(left);

            TryAppend(right);
        }

        public bool TryAppend(char value)
        {
            if (Left == null)
            {
                Left = value;

                if (value.IsHalfWidth() == false)
                {
                    Right = '\0';
                }

                return true;
            }

            else if (Right == null)
            {
                if (value.IsHalfWidth() == false)
                {
                    Right = ' ';
                }

                else
                {
                    Right = value;

                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Left}{Right}";
        }
    }
}