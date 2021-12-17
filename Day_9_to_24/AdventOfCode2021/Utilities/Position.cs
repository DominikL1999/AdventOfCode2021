using System;

namespace Utilities
{
    public class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        { X = x; Y = y; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);
        }

        public bool Equals(Position other)
        {
            return other != null && X ==
                other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}