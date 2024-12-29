namespace ZL.CS.FW
{
    public sealed class Foreground : Graphic
    {
        public const byte defaultColor = ANSI.fgDefaultColor;

        public readonly Char2[,] char2Map;

        public Foreground(params string[] strings) : this(null, strings.ToChar2Map()) { }

        public Foreground(byte[,] colorMap, params string[] strings) : this(colorMap, strings.ToChar2Map()) { }

        private Foreground(byte[,]? colorMap, Char2[,] char2Map) : base(char2Map.GetSize(), colorMap)
        {
            this.char2Map = char2Map;
        }
    }
}