namespace ZL.CS
{
    public static partial class CharExtensions
    {
        public static bool IsHalfWidth(this char instance)
        {
            return ('\u0020' <= instance && instance <= '\u007E') || ('\uFF61' <= instance && instance <= '\uFF9F');
        }
    }
}