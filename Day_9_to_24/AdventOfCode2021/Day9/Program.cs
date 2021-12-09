using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
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

    internal class Program
    {
        private static void AddLine(int[,] heightMap, int lineNumber, string line)
        {
            for (int i = 0; i < line.Length; i++)
                heightMap[lineNumber, i] = Convert.ToInt32(char.GetNumericValue(line[i]));
        }

        private static bool IsLowPoint(int[,] heightMap, int X, int Y)
        {
            List<Position> neighbours = new()
            {
                new(X, Y - 1),
                new(X, Y + 1),
                new(X - 1, Y),
                new(X + 1, Y)
            };

            neighbours.RemoveAll(pos => pos.X < 0 || pos.X > heightMap.GetLength(0) - 1
                || pos.Y < 0 || pos.Y > heightMap.GetLength(1) - 1);

            return neighbours.TrueForAll(pos => heightMap[pos.X, pos.Y] > heightMap[X, Y]);
        }

        private static int RiskLevel(int height)
        {
            return height + 1;
        }

        private static void Main()
        {
            // Input
            var xCount = File.ReadLines("day9_input.txt").Count();
            using var streamReader = new StreamReader("day9_input.txt");
            string line = streamReader.ReadLine();
            int yCount = line.Length;
            int[,] heightMap = new int[xCount, yCount];
            AddLine(heightMap, 0, line);
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    heightMap[x, y] = 0;
                }
            }

            AddLine(heightMap, 0, line);
            for (int i = 1; i < xCount; i++)
            {
                line = streamReader.ReadLine();
                AddLine(heightMap, i, line);
            }

            // Solution 1
            int sum = 0;
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    if (IsLowPoint(heightMap, x, y))
                    {
                        sum += RiskLevel(heightMap[x, y]);
                    }
                }
            }
            Console.WriteLine($"Sum: {sum}");

            // Solution 2
            int[,] basinMap = new int[heightMap.GetLength(0), heightMap.GetLength(1)];
            for (int x = 0; x < basinMap.GetLength(0); x++)
            {
                for (int y = 0; y < basinMap.GetLength(1); y++)
                {
                    if (heightMap[x, y] == 9) basinMap[x, y] = -1;
                }
            }
            for (int x = 0; x < basinMap.GetLength(0); x++)
            {
                for (int y = 0; y < basinMap.GetLength(1); y++)
                {
                    //if (!IsInBasin(basinMap, x, y) && basinMap)
                }
            }

            Console.WriteLine($"Sum of risk level: {sum}");
        }
    }
}