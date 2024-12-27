namespace ZL.CS.API
{
    public struct CharCell
    {
        public char? Left { get; private set; } = null;

        public char? Right { get; private set; } = null;

        public CharCell(char left)
        {
            TryAppend(left);
        }

        public CharCell(char left, char right)
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