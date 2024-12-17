namespace ZL.CS
{
    public static class CharExtension
    {
        public static bool IsHalfWidth(this char instance)
        {
            return ('\u0020' <= instance && instance <= '\u007E') || ('\uFF61' <= instance && instance <= '\uFF9F');
        }
    }
}