using System.Collections.Generic;

using System.Drawing;

namespace ZL.CS.ConsoleEngine
{
    public sealed class Transform
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
            get => position.Location;

            set => Move(value.Direction(position.Location));
        }

        public Point LocalLocation
        {
            get => localPosition.Location;

            set => Move(value.Direction(localPosition.Location));
        }

        public int Depth
        {
            get => position.Depth;

            set => Move(value - position.Depth);
        }

        public int LocalDepth
        {
            get => localPosition.Depth;

            set => Move(value - localPosition.Depth);
        }

        public readonly SceneObject sceneObject;

        private Transform? parent = null;

        public Transform? Parent
        {
            get => parent;

            set
            {
                if (value != null)
                {
                    localPosition -= value.Position;
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

        internal Transform(SceneObject sceneObject)
        {
            this.sceneObject = sceneObject;
        }

        public void Move(Position position)
        {
            this.position += position;

            foreach (var child in children)
            {
                child.Move(position);
            }
        }

        public void Move(Size direction)
        {
            position.Location += direction;

            localPosition.Location += direction;

            foreach (var child in children)
            {
                child.Move(direction);
            }
        }

        public void Move(int depth)
        {
            position.Depth += depth;

            localPosition.Depth += depth;

            foreach (var child in children)
            {
                child.Move(depth);
            }
        }
    }
}