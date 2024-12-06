using System.Drawing;

namespace ZL.CS.Graphics
{
    public sealed class Foreground : Graphic
    {
        public readonly string[] textMap;

        public Foreground(int[,] colorMap, params string[] textMap) : base(colorMap)
        {
            this.textMap = textMap;
        }

        public Foreground(params string[] textMap) : base(textMap.GetSize())
        {
            this.textMap = textMap;
        }

        public override void Draw(Canvas canvas, Point point, sbyte depth)
        {
            canvas.Draw(this, point, depth);
        }
    }
}