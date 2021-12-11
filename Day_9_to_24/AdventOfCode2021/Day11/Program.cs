using System;
using System.Collections.Generic;
using System.IO;

namespace Day11
{
    internal class Program
    {
        // Constants
        private const int STEPS = 100;

        internal class Position
        {
            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        private static void Main()
        {
            // Read Input
            string[] lines = File.ReadAllLines("input.txt");
            int[,] octopi = new int[lines.Length, Convert.ToInt32(lines[0].Length)]; // "octopi" is the plural of octopus
            int[,] octopi2 = new int[lines.Length, Convert.ToInt32(lines[0].Length)];

            for (int x = 0; x < octopi.GetLength(0); x++)
                for (int y = 0; y < octopi.GetLength(1); y++)
                {
                    octopi[x, y] = Convert.ToInt32(lines[x][y].ToString());
                    octopi2[x, y] = octopi[x, y];
                }

            // Solution
            int flashes = Solution1(octopi);
            int steps = Solution2(octopi2);

            Console.WriteLine($"Number of flashes: {flashes}");
            Console.WriteLine($"First step: {steps}");
        }

        private static int Solution1(int[,] octopi)
        {
            int flashes = 0;
            for (int i = 0; i < STEPS; i++)
                flashes += DoStep(octopi);
            return flashes;
        }

        private static int Solution2(int[,] octopi2)
        {
            for (int step = 1; ; step++)
            {
                if (DoStep(octopi2) == octopi2.GetLength(0) * octopi2.GetLength(1))
                    return step;
            }
        }

        private static int DoStep(int[,] octopi)
        {
            Stack<Position> flashingOctopi = new();
            int flashingCount = 0;

            // Increase all by 1 and save flashingOctopi
            for (int x = 0; x < octopi.GetLength(0); x++)
            {
                for (int y = 0; y < octopi.GetLength(1); y++)
                {
                    if (++octopi[x, y] >= 10)
                    {
                        flashingOctopi.Push(new(x, y));
                        flashingCount++;
                    }
                }
            }

            // Flash all flashing octopi
            while (flashingOctopi.Count > 0)
            {
                Position octopus = flashingOctopi.Pop();
                octopi[octopus.X, octopus.Y] = 0;
                var neighbours = GetValidNeighbours(octopi.GetLength(0) - 1, octopi.GetLength(1) - 1, octopus);
                foreach (var neighbour in neighbours)
                    if (octopi[neighbour.X, neighbour.Y] > 0)
                    {
                        if (++octopi[neighbour.X, neighbour.Y] == 10)
                        {
                            flashingOctopi.Push(neighbour);
                            flashingCount++;
                        }
                    }
            }
            return flashingCount;
        }

        private static List<Position> GetValidNeighbours(int maxX, int maxY, Position p)
        {
            List<Position> neighbours = new();
            for (int x = Math.Max(0, p.X - 1); x <= Math.Min(maxX, p.X + 1); x++)
                for (int y = Math.Max(0, p.Y - 1); y <= Math.Min(maxY, p.Y + 1); y++)
                    neighbours.Add(new(x, y));
            return neighbours;
        }
    }
}