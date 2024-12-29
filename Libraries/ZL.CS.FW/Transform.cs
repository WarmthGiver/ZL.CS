using System.Collections.Generic;

using System.Numerics;

namespace ZL.CS.FW
{
    public class Transform
    {
        private Vector3 position;

        public Vector3 Position
        {
            get => position;

            set => Move(value - position);
        }

        private Vector3 localPosition;

        public Vector3 LocalPosition
        {
            get => localPosition;

            set => Move(value - localPosition);
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

        internal Transform(Vector3 position, Transform? parent)
        {
            this.position = position;

            Parent = parent;
        }

        public virtual void Move(Vector3 position)
        {
            this.position += position;

            localPosition += position;

            foreach (var child in children)
            {
                child.Move(position);
            }
        }
    }
}