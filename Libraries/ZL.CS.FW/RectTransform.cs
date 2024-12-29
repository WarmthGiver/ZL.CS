using System.Drawing;

using System.Numerics;

namespace ZL.CS.FW
{
    public sealed class RectTransform : Transform
    {
        private RectangleF rect;

        public RectangleF Rect => rect;

        public SizeF Size
        {
            get => rect.Size;

            set
            {
                Pivot = value.GetPivot();

                rect.Location = Position.ToPointF() - Pivot;

                rect.Size = value;
            }
        }

        public SizeF Pivot { get; private set; }

        internal RectTransform(Transform @base) : base(@base.Position, @base.Parent) { }

        public override void Move(Vector3 position)
        {
            rect.Location += position.ToSizeF();

            base.Move(position);
        }
    }
}