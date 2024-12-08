using System.Drawing;

namespace ZL.CS.Graphics
{
    public sealed class Background : Graphic
    {
        public const byte defaultColor = 0;

        public Background(byte[,] colorMap) : base(colorMap) { }

        public override void Draw(Canvas canvas, Point point, byte depth)
        {
            canvas.DrawRequest(this, point, depth);
        }
    }
}