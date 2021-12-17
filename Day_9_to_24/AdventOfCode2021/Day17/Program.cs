using System;
using System.Collections.Generic;
using Utilities;

namespace Day17
{
    internal class Program
    {
        //private const int xMin = 20;
        //private const int xMax = 30;
        //private const int yMin = -10;
        //private const int yMax = -5;

        private const int xMin = 14;
        private const int xMax = 50;
        private const int yMin = -267;
        private const int yMax = -225;

        private static void Main()
        {
            // Solution
            int highestY = HighestY();
            int count = CountSolutions();
            Console.WriteLine($"highest y in any solution: {highestY}");
            Console.WriteLine($"Number of feasible solutions: {count}");
        }

        private static int CountSolutions()
        {
            int count = 0;
            for (int yVel = -yMin; yVel >= yMin; yVel--)
            {
                for (int xVel = 1; xVel <= xMax; xVel++)
                {
                    if (IsSolution(xVel, yVel, out int highestY))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static int HighestY()
        {
            int maxHighestY = int.MinValue;
            for (int yVel = -yMin; yVel >= yMin; yVel--)
            {
                for (int xVel = 1; xVel <= xMax; xVel++)
                {
                    if (IsSolution(xVel, yVel, out int highestY))
                    {
                        //Console.WriteLine($"Solution: {xVel}, {yVel}");
                        //Console.WriteLine($"Highest y in the solution: {highestY}");
                        maxHighestY = Math.Max(maxHighestY, highestY);
                        //return highestY;
                    }
                }
            }

            return maxHighestY;
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

        private static void DrawBoard()
        {
            for (int x = 0; x <= xMax; x++)
            {
                for (int y = 0; y >= yMin; y--)
                {
                    if (TargetHit(new(x, y)))
                        Console.Write("T");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private static void DrawPath(HashSet<Position> positions)
        {
            for (int x = 0; x <= xMax; x++)
            {
                for (int y = 0; y >= yMin; y--)
                {
                    if (positions.Contains(new(x, y)))
                        Console.Write("#");
                    else if (TargetHit(new(x, y)))
                        Console.Write("T");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
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