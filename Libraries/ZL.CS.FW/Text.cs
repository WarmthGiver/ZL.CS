namespace ZL.CS.FW
{
    public sealed class Text : GUI
    {
        public const byte defaultColor = 007;

        public readonly Char2[,] char2Map;

        public Text(params string[] strings) : this(null, strings.ToChar2Map()) { }

        public Text(byte[] colorMap, params string[] strings) : this(colorMap, strings.ToChar2Map()) { }

        private Text(byte[]? colorMap, Char2[,] char2Map) : base(colorMap)
        {
            this.char2Map = char2Map;
        }
    }
}