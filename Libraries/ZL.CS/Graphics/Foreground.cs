using System;

namespace ZL.CS.Graphics
{
    public sealed class Foreground : Graphic
    {
        public const byte defaultColor = (int)ConsoleColor.Gray;

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
            Camera.main?.DrawCall(this, position);
        }
    }
}