using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    internal class Program
    {
        private const int STEPS = 10;

        private static Dictionary<string, string> insertionRules = new();

        private static void Main()
        {
            // Input
            var lines = File.ReadAllLines("input.txt");
            for (int i = 2; i < lines.Length; i++)
            {
                var rule = lines[i].Split(" -> ");
                insertionRules.Add(rule[0], rule[1]);
            }

            // Solution 1
            string polymer = lines[0];
            for (int i = 0; i < STEPS; i++)
            {
                polymer = DoStep(polymer);
            }
            IEnumerable<IGrouping<char, char>> grouped = polymer.GroupBy(c => c);
            int maxCount = grouped.Max(group => group.Count());
            int minCount = grouped.Min(group => group.Count());

            Console.WriteLine($"Difference in quantity of most common and least common element: {maxCount - minCount}");
        }

        private static string DoStep(string polymer)
        {
            List<Tuple<int, string>> insertions = new();
            for (int i = 1; i < polymer.Length; i++)
            {
                if (insertionRules.TryGetValue(string.Concat(polymer[i - 1], polymer[i]), out string sub))
                {
                    insertions.Add(new(i, sub));
                }
            }

            insertions.Reverse();
            foreach (var insertion in insertions)
            {
                polymer = polymer.Insert(insertion.Item1, insertion.Item2);
            }

            return polymer;
        }
    }
}