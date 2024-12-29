using System.Numerics;

namespace ZL.CS.FW
{
    public abstract class GUI : IDrawable
    {
        public readonly byte[]? colorMap = null;

        public GUIPivot Pivot { get; set; } = GUIPivot.LeftTop;

        protected GUI(byte[]? colorMap = null)
        {
            this.colorMap = colorMap;
        }

        public void Draw(Vector3 position)
        {
            Camera.Main?.Draw(this, position);
        }
    }

    public enum GUIPivot
    {
        LeftTop,

        LeftMiddle,

        LeftBottom,

        MiddleTop,

        Middle,

        MiddleBottom,

        RightTop,

        RightMiddle,

        RightBottom
    }
}