using System.Drawing;

namespace ZL.CS.Graphics
{
    public sealed class Background : Graphic
    {
        public Background(int[,] colorMap) : base(colorMap) { }

        public override void Draw(Canvas canvas, Point point, sbyte depth)
        {
            canvas.Draw(this, point, depth);
        }
    }
}