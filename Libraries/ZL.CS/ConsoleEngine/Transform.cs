using System.Collections.Generic;

using System.Drawing;

namespace ZL.CS.ConsoleEngine
{
    public class Transform
    {
        private Position position;

        public Position Position
        {
            get => position;

            set => Move(value - position);
        }

        private Position localPosition;

        public Position LocalPosition
        {
            get => localPosition;

            set => Move(value - localPosition);
        }

        public Point Location
        {
            get => position.location;

            set => Move(value.Direction(position.location));
        }

        public Point LocalLocation
        {
            get => localPosition.location;

            set => Move(value.Direction(localPosition.location));
        }

        public int Depth
        {
            get => position.depth;

            set => Move(value - position.depth);
        }

        public int LocalDepth
        {
            get => localPosition.depth;

            set => Move(value - localPosition.depth);
        }

        private Transform? parent = null;

        public Transform? Parent
        {
            get => parent;

            set
            {
                if (value == parent)
                {
                    return;
                }

                if (value != null)
                {
                    value.children.AddLast(this);

                    localPosition = position - value.position;
                }

                else if (parent != null)
                {
                    parent.children.Remove(this);

                    localPosition = position;
                }

                parent = value;
            }
        }

        private readonly LinkedList<Transform> children = new();

        internal Transform(Position position, Transform? parent)
        {
            this.position = position;

            Parent = parent;
        }

        public virtual void Move(Position position)
        {
            this.position += position;

            localPosition += position;

            foreach (var child in children)
            {
                child.Move(position);
            }
        }

        public virtual void Move(Size direction)
        {
            position.location += direction;

            localPosition.location += direction;

            foreach (var child in children)
            {
                child.Move(direction);
            }
        }

        public void Move(int depth)
        {
            position.depth += depth;

            localPosition.depth += depth;

            foreach (var child in children)
            {
                child.Move(depth);
            }
        }
    }
}