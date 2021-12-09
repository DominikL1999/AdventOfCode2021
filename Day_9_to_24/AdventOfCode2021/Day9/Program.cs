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

        private static bool IsLowPoint(int[,] heightMap, Position pos)
        {
            List<Position> neighbours = GetValidNeighbours(heightMap.GetLength(0), heightMap.GetLength(1), pos);

            return neighbours.TrueForAll(p => heightMap[p.X, p.Y] > heightMap[pos.X, pos.Y]);
        }

        private static List<Position> GetValidNeighbours(int maxX, int maxY, Position pos)
        {
            List<Position> neighbours = new()
            {
                new(pos.X, pos.Y - 1),
                new(pos.X, pos.Y + 1),
                new(pos.X - 1, pos.Y),
                new(pos.X + 1, pos.Y)
            };

            neighbours.RemoveAll(p => p.X < 0 || p.X > maxX - 1
                || p.Y < 0 || p.Y > maxY - 1);

            return neighbours;
        }

        private static bool IsInBasin(int[,] basinMap, Position p)
        {
            return basinMap[p.X, p.Y] != -1;
        }

        private static int CreateBasin(int[,] basinMap, int[,] heightMap, Position pos, int basinNumber)
        {
            if (IsInBasin(basinMap, pos)) return 0;

            basinMap[pos.X, pos.Y] = basinNumber;
            List<Position> validNeighbours = GetValidNeighbours(basinMap.GetLength(0), basinMap.GetLength(1), pos);
            validNeighbours.RemoveAll(p => heightMap[p.X, p.Y] < heightMap[pos.X, pos.Y] || heightMap[p.X, p.Y] == 9);

            int basinSize = 1;
            foreach (var neighbour in validNeighbours)
            {
                basinSize += CreateBasin(basinMap, heightMap, neighbour, basinNumber);
            }
            return basinSize;
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
            List<Position> lowPoints = new();
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    if (IsLowPoint(heightMap, new(x, y)))
                    {
                        lowPoints.Add(new(x, y));
                        sum += heightMap[x, y] + 1;
                    }
                }
            }

            Console.WriteLine($"Sum of risk level: {sum}");

            // Solution 2 Init basinMap
            int[,] basinMap = new int[heightMap.GetLength(0), heightMap.GetLength(1)];
            for (int x = 0; x < basinMap.GetLength(0); x++)
            {
                for (int y = 0; y < basinMap.GetLength(1); y++)
                {
                    basinMap[x, y] = -1;
                }
            }

            // Create basins
            int basinNumber = 0;
            SortedSet<int> basinSizes = new();
            foreach (var lowPoint in lowPoints)
            {
                int basinSize = CreateBasin(basinMap, heightMap, lowPoint, basinNumber++);
                basinSizes.Add(basinSize);
            }

            // Calculate solution
            var largestThree = basinSizes.Reverse().Take(3);
            int solution = 1;
            foreach (var factor in largestThree)
                solution *= factor;
            Console.WriteLine($"Solution: {solution}");
        }
    }
}