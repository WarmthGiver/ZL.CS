namespace ZL.CS.Graphics
{
    public sealed class Background : Graphic
    {
        public const byte defaultColor = 000;

        public Background(byte[,] colorMap) : base(colorMap) { }
    }
}