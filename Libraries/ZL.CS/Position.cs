using System.Drawing;

namespace ZL.CS
{
    public struct Position
    {
        public Point Location { get; set; }

        public int Depth { get; set; }

        public Position() : this(new(0, 0), 0) { }

        public Position(int x, int y, int z) : this(new(x, y), z) { }

        public Position(Point location, int depth)
        {
            Location = location;

            Depth = depth;
        }

        public static Position operator +(Position left, Position right)
        {
            return new(left.Location.Add(right.Location), left.Depth + right.Depth);
        }

        public static Position operator -(Position left, Position right)
        {
            return new(left.Location.Sub(right.Location), left.Depth - right.Depth);
        }
    }
}