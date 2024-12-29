namespace ZL.CS.FW
{
    public sealed class Background : Graphic
    {
        public const byte defaultColor = ANSI.bgDefaultColor;

        public Background(byte[,] colorMap) : base(colorMap) { }
    }
}