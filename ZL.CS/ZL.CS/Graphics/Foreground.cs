using System.Drawing;

namespace ZL.CS.Graphics
{
    public sealed class Foreground : Graphic
    {
        public const byte defaultColor = 7;

        public readonly string[] textMap;

        public Foreground(byte[,] colorMap, params string[] textMap) : base(colorMap)
        {
            this.textMap = textMap;
        }
        public Foreground(params string[] textMap) : base(textMap.GetSize())
        {
            this.textMap = textMap;
        }

        public override void Draw(Canvas canvas, Point point, byte depth)
        {
            canvas.DrawRequest(this, point, depth);
        }
    }
}