using System;
using System.Collections.Generic;
using System.IO;

namespace Day12
{
    internal class Program
    {
        private const string START = "start";
        private const string END = "end";

        private static readonly Dictionary<string, List<string>> neighbours = new();

        private static void Main()
        {
            // Input
            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                string[] data = line.Split('-');
                if (data[1] != START)
                    AddNeighbour(neighbours, data[0], data[1]);
                if (data[0] != START)
                    AddNeighbour(neighbours, data[1], data[0]);
            }

            // Solution
            int pathCount = GetPathCount(START, new());
            Console.WriteLine($"Number of paths from START to END: {pathCount}");
            int pathCount2 = GetPathCount2(START, new(), null);
            Console.WriteLine($"2: Number of paths from START to END: {pathCount2}");
        }

        private static int GetPathCount(string cave, HashSet<string> visitedSmallCaves)
        {
            if (cave == END) return 1;

            int pathCount = 0;
            foreach (var neighbour in neighbours[cave])
            {
                if (IsBigCave(neighbour))
                    pathCount += GetPathCount(neighbour, visitedSmallCaves);
                else if (!visitedSmallCaves.Contains(neighbour))
                {
                    visitedSmallCaves.Add(neighbour);
                    pathCount += GetPathCount(neighbour, visitedSmallCaves);
                    visitedSmallCaves.Remove(neighbour);
                }
            }
            return pathCount;
        }

        private static int GetPathCount2(string cave, HashSet<string> visitedSmallCaves, string smallCave)
        {
            if (cave == END) return 1;

            int pathCount = 0;
            foreach (var neighbour in neighbours[cave])
            {
                if (IsBigCave(neighbour))
                    pathCount += GetPathCount2(neighbour, visitedSmallCaves, smallCave);
                else if (!visitedSmallCaves.Contains(neighbour))
                {
                    visitedSmallCaves.Add(neighbour);
                    pathCount += GetPathCount2(neighbour, visitedSmallCaves, smallCave);
                    visitedSmallCaves.Remove(neighbour);
                }
                else if (smallCave == null)
                {
                    smallCave = neighbour;
                    pathCount += GetPathCount2(neighbour, visitedSmallCaves, smallCave);
                    smallCave = null;
                }
            }
            return pathCount;
        }

        private static bool IsBigCave(string cave)
        {
            return char.IsUpper(cave[0]);
        }

        private static void AddNeighbour(Dictionary<string, List<string>> neighbours, string cave, string neighbour)
        {
            if (cave == neighbour) throw new ApplicationException("Cave cannot be a neighbour of itself.");

            bool found = neighbours.TryGetValue(cave, out List<string> list);
            if (found) list.Add(neighbour);
            else neighbours.Add(cave, new() { neighbour });
        }
    }
}