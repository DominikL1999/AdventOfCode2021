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

        private static bool IsLowPoint(int[,] heightMap, int x, int y)
        {
            List<Position> neighbours = GetValidNeighbours(heightMap.GetLength(0), heightMap.GetLength(1), x, y);

            return neighbours.TrueForAll(pos => heightMap[pos.X, pos.Y] > heightMap[x, y]);
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
            List<Position> lowPoints = new();
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    if (IsLowPoint(heightMap, x, y))
                    {
                        lowPoints.Add(new(x, y));
                        sum += RiskLevel(heightMap[x, y]);
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
            foreach (var lowPoint in lowPoints)
                CreateBasin(basinMap, heightMap, lowPoint.X, lowPoint.Y, basinNumber++);
            //for (int x = 0; x < basinMap.GetLength(0); x++)
            //{
            //    for (int y = 0; y < basinMap.GetLength(1); y++)
            //    {
            //        if (heightMap[x, y] != 9 && !IsInBasin(basinMap, x, y))
            //            CreateBasin(basinMap, heightMap, x, y, basinNumber++);
            //    }
            //}

            // Get solution
            Dictionary<int, int> basinSizes = new();
            for (int x = 0; x < basinMap.GetLength(0); x++)
            {
                for (int y = 0; y < basinMap.GetLength(1); y++)
                {
                    if (IsInBasin(basinMap, x, y))
                    {
                        if (basinSizes.ContainsKey(basinMap[x, y]))
                        {
                            basinSizes[basinMap[x, y]]++;
                        }
                        else basinSizes.Add(basinMap[x, y], 1);
                    }
                }
            }
            PrintMap(basinMap);
            var basinSizesSorted = basinSizes.ToList();
            basinSizesSorted.Sort((pair1, pair2) => pair2.Value - pair1.Value);

            var largestThree = basinSizesSorted.Take(3).Select(pair => pair.Value);
            int solution = 1;
            foreach (var factor in largestThree)
                solution *= factor;
            Console.WriteLine($"Solution: {solution}");
        }

        private static void PrintMap(int[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        private static bool IsInBasin(int[,] basinMap, int x, int y)
        {
            return basinMap[x, y] != -1;
        }

        private static void CreateBasin(int[,] basinMap, int[,] heightMap, int x, int y, int basinNumber)
        {
            if (IsInBasin(basinMap, x, y)) return;

            basinMap[x, y] = basinNumber;
            List<Position> validNeighbours = GetValidNeighbours(basinMap.GetLength(0), basinMap.GetLength(1), x, y);
            validNeighbours.RemoveAll(pos => heightMap[pos.X, pos.Y] < heightMap[x, y] || heightMap[pos.X, pos.Y] == 9);

            foreach (var neighbour in validNeighbours)
            {
                CreateBasin(basinMap, heightMap, neighbour.X, neighbour.Y, basinNumber);
            }
        }

        private static List<Position> GetValidNeighbours(int maxX, int maxY, int x, int y)
        {
            List<Position> neighbours = new()
            {
                new(x, y - 1),
                new(x, y + 1),
                new(x - 1, y),
                new(x + 1, y)
            };

            neighbours.RemoveAll(pos => pos.X < 0 || pos.X > maxX - 1
                || pos.Y < 0 || pos.Y > maxY - 1);

            return neighbours;
        }
    }
}