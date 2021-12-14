using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    internal class Problem : IEquatable<Problem>
    {
        public string Polymer { get; set; }
        public long StepsLeft { get; set; }

        public Problem(string polymer, long stepsLeft)
        {
            Polymer = polymer ?? throw new ArgumentNullException(nameof(polymer));
            StepsLeft = stepsLeft;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Problem);
        }

        public bool Equals(Problem other)
        {
            return other != null &&
                   Polymer == other.Polymer &&
                   StepsLeft == other.StepsLeft;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Polymer, StepsLeft);
        }
    }

    internal class Program
    {
        private const int STEPS = 10;
        private const int STEPS2 = 40;

        // todo: change type to Dictionary<string, char>
        private static Dictionary<string, string> insertionRules = new();

        private static readonly Dictionary<Problem, Dictionary<char, long>> solutions = new();

        private static void Main()
        {
            // TEST
            Dictionary<Problem, int> testDict = new();
            Problem p1 = new("test", 10);
            testDict.Add(p1, 10);
            Console.WriteLine($"Test contains p1: {testDict.ContainsKey(p1)}");

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
            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key}, {group.Count()}");
            }
            int minCount = grouped.Min(group => group.Count());
            int maxCount = grouped.Max(group => group.Count());

            Console.WriteLine($"Difference in quantity of most common and least common element (10 steps): {maxCount - minCount}");
            Console.WriteLine("===============================");

            // Solution 2
            Dictionary<char, long> groups = GroupSizes(new(lines[0], STEPS2));
            foreach (var entry in groups)
            {
                Console.WriteLine($"{entry.Key} {entry.Value}");
            }
            long minCount2 = groups.Min(pair => pair.Value);
            long maxCount2 = groups.Max(pair => pair.Value);
            Console.WriteLine($"Difference: {maxCount2 - minCount2}");
        }

        private static Dictionary<char, long> GroupSizes(Problem problem)
        {
            // todo: rename to solution
            Dictionary<char, long> result;

            if (solutions.TryGetValue(problem, out result)) return result;

            if (problem.StepsLeft == 0)
            {
                result = CreateDictFromPolymer(problem.Polymer);
                solutions.Add(problem, result);
                return result;
            }

            result = new();

            for (int i = 0; i < problem.Polymer.Length - 1; i++)
            {
                string polymer = problem.Polymer.Substring(i, 2);
                if (insertionRules.ContainsKey(polymer))
                {
                    string sub = insertionRules[polymer];
                    Problem problem1 = new(string.Concat(polymer[0], sub), problem.StepsLeft - 1);
                    Problem problem2 = new(string.Concat(sub, polymer[1]), problem.StepsLeft - 1);
                    Dictionary<char, long> solution1;
                    Dictionary<char, long> solution2;
                    // Solve for problem 1
                    if (solutions.ContainsKey(problem1))
                    {
                        solution1 = solutions[problem1];
                    }
                    else
                    {
                        solution1 = GroupSizes(problem1);
                        solutions.TryAdd(problem1, solution1);
                    }

                    //Solve for problem 2
                    if (solutions.ContainsKey(problem2))
                    {
                        solution2 = solutions[problem2];
                    }
                    else
                    {
                        solution2 = GroupSizes(problem2);
                        solutions.TryAdd(problem2, solution2);
                    }

                    // Join solutions for problem 1 and problem 2
                    result = SumDicts(result, solution1);
                    result = SumDicts(result, solution2);
                    result[problem1.Polymer[0]]--;
                    result[problem2.Polymer[0]]--;
                }
                else
                {
                    result = SumDicts(result, CreateDictFromPolymer(polymer));
                }
            }
            result[problem.Polymer[0]]++;

            return result;
        }

        private static Dictionary<char, long> SumDicts(Dictionary<char, long> dict1, Dictionary<char, long> dict2)
        {
            Dictionary<char, long> result = new();
            foreach (var entry in dict1)
            {
                AddToDict(result, entry.Key, entry.Value);
            }
            foreach (var entry in dict2)
            {
                AddToDict(result, entry.Key, entry.Value);
            }
            return result;
        }

        private static void AddToDict(Dictionary<char, long> dict, char c, long x)
        {
            if (!dict.TryAdd(c, x))
                dict[c] += x;
        }

        private static Dictionary<char, long> CreateDictFromPolymer(string polymer)
        {
            Dictionary<char, long> result = new();
            foreach (var c in polymer)
                AddToDict(result, c, 1);
            return result;
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