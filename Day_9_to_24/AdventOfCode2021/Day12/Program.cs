using System;
using System.Collections.Generic;
using System.IO;

namespace Day12
{
    internal class Program
    {
        private const string START = "start";
        private const string END = "end";

        private static void Main()
        {
            // Input
            string[] lines = File.ReadAllLines("input.txt");
            Dictionary<string, List<string>> neighbours = new();
            foreach (var line in lines)
            {
                string[] data = line.Split('-');
                AddNeighbour(neighbours, data[0], data[1]);
                AddNeighbour(neighbours, data[1], data[0]);
            }

            // Solution
            int pathCount = GetSolution(neighbours, START);
            Console.WriteLine($"Number of paths from START to END: {pathCount}");
        }

        private static int GetSolution(Dictionary<string, List<string>> neighbours, string start)
        {
            return GetPathCount(neighbours, start, new());
        }

        private static int GetPathCount(Dictionary<string, List<string>> neighbours, string cave, HashSet<string> visitedSmallCaves)
        {
            if (cave == END) return 1;

            int pathCount = 0;
            foreach (var neighbour in neighbours[cave])
            {
                if (IsBigCave(cave))
                    pathCount += GetPathCount(neighbours, neighbour, visitedSmallCaves);
                else if (IsSmallCave(cave) && !visitedSmallCaves.Contains(cave))
                {
                    visitedSmallCaves.Add(cave);
                    pathCount += GetPathCount(neighbours, neighbour, visitedSmallCaves);
                    visitedSmallCaves.Remove(cave);
                }
            }
            return pathCount;
        }

        private static bool IsBigCave(string cave)
        {
            return char.IsUpper(cave[0]);
        }

        private static bool IsSmallCave(string cave)
        {
            return char.IsLower(cave[0]);
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