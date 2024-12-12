using System.Drawing;

namespace ZL.CS
{
    public struct Position
    {
        public Point location;

        public int depth;

        public Position() : this(new(0, 0), 0) { }

        public Position(int x, int y, int z) : this(new(x, y), z) { }

        public Position(Point location, int depth)
        {
            this.location = location;

            this.depth = depth;
        }

        public static Position operator +(Position left, Position right)
        {
            return new(left.location.Add(right.location), left.depth + right.depth);
        }

        public static Position operator -(Position left, Position right)
        {
            return new(left.location.Sub(right.location), left.depth - right.depth);
        }
    }
}