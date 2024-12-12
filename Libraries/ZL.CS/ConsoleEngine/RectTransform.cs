﻿using System.Drawing;

namespace ZL.CS.ConsoleEngine
{
    public sealed class RectTransform : Transform
    {
        private Rectangle rect;

        public Rectangle Rect => rect;

        public Size Size
        {
            get => rect.Size;

            set
            {
                Pivot = value.GetHalf();

                rect.Location = sceneObject.Transform.Location - Pivot;

                rect.Size = value;
            }
        }

        public Size Pivot { get; private set; }

        public RectTransform(Transform transform) : base(transform.sceneObject)
        {
            Position = transform.Position;
        }

        public override void Move(Position position)
        {
            rect.Location = rect.Location.Add(position.location);

            base.Move(position);
        }

        public override void Move(Size direction)
        {
            rect.Location += direction;

            base.Move(direction);
        }
    }
}