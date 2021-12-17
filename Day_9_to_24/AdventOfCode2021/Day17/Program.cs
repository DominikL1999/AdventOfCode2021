using System;
using Utilities;

namespace Day17
{
    internal class Program
    {
        private const int xMin = 14;
        private const int xMax = 50;
        private const int yMin = -267;
        private const int yMax = -225;

        private static void Main()
        {
            // Solution
            int maxHighestY = int.MinValue;
            int count = 0;
            for (int yVel = -yMin; yVel >= yMin; yVel--)
            {
                for (int xVel = 1; xVel <= xMax; xVel++)
                {
                    if (IsSolution(xVel, yVel, out int highestY))
                    {
                        maxHighestY = Math.Max(maxHighestY, highestY);
                        count++;
                    }
                }
            }

            Console.WriteLine($"highest y in any solution: {maxHighestY}");
            Console.WriteLine($"Number of feasible solutions: {count}");
        }

        private static bool IsSolution(int xVel, int yVel, out int highestY)
        {
            highestY = 0;
            Position pos = new(0, 0);
            while (!OutOfBounds(pos))
            {
                pos.X += xVel;
                xVel--;
                xVel = Math.Max(0, xVel);
                pos.Y += yVel;
                yVel--;
                highestY = Math.Max(highestY, pos.Y);
                if (TargetHit(pos))
                    return true;
                else if (OutOfBounds(pos))
                    return false;
            }

            return false;
        }

        private static bool TargetHit(Position pos)
        {
            return pos.X >= xMin && pos.X <= xMax && pos.Y >= yMin && pos.Y <= yMax;
        }

        private static bool OutOfBounds(Position pos)
        {
            return pos.X >= xMax || pos.Y <= yMin;
        }
    }
}