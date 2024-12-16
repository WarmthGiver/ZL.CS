namespace ZL.CS.Graphics
{
    public sealed class Foreground : Graphic
    {
        public const byte defaultColor = 007;

        public readonly char[,] textMap;

        public Foreground(byte[,] colorMap, params string[] textMap) : base(colorMap)
        {
            this.textMap = textMap.ToChar();
        }

        public Foreground(params string[] textMap) : base(textMap.GetSize())
        {
            this.textMap = textMap.ToChar();
        }

        public override void DrawCall(Position position)
        {
            Camera.Main?.DrawCall(this, position);
        }
    }
}