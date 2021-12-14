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
        private const int STEPS1 = 10;
        private const int STEPS2 = 40;

        private static readonly Dictionary<string, char> insertionRules = new();
        private static readonly Dictionary<Problem, Dictionary<char, long>> solutions = new();

        private static void Main()
        {
            // Input
            var lines = File.ReadAllLines("input.txt");
            for (int i = 2; i < lines.Length; i++)
            {
                var rule = lines[i].Split(" -> ");
                insertionRules.Add(rule[0], rule[1].Single());
            }

            // Solution 1
            Dictionary<char, long> groups1 = GroupSizes(new(lines[0], STEPS1));
            long solution1 = GetSolution(groups1);
            Console.WriteLine($"Solution 2 ({STEPS1} steps): {solution1}");

            // Solution 2
            Dictionary<char, long> groups2 = GroupSizes(new(lines[0], STEPS2));
            long solution2 = GetSolution(groups2);
            Console.WriteLine($"Solution 2 ({STEPS2} steps): {solution2}");
        }

        private static long GetSolution(Dictionary<char, long> groups)
        {
            long min = groups.Min(pair => pair.Value);
            long max = groups.Max(pair => pair.Value);
            return max - min;
        }

        private static Dictionary<char, long> GroupSizes(Problem problem)
        {
            Dictionary<char, long> solution = new();

            if (problem.StepsLeft == 0)
            {
                solution = CreateDictFromPolymer(problem.Polymer);
                solutions.Add(problem, solution);
                return solution;
            }

            // Iterate over each pair of characters
            for (int i = 0; i < problem.Polymer.Length - 1; i++)
            {
                string polymer = problem.Polymer.Substring(i, 2);
                if (insertionRules.ContainsKey(polymer))
                {
                    char sub = insertionRules[polymer];
                    Problem problem1 = new(string.Concat(polymer[0], sub), problem.StepsLeft - 1);
                    Problem problem2 = new(string.Concat(sub, polymer[1]), problem.StepsLeft - 1);
                    // Solve for problem 1
                    if (!solutions.TryGetValue(problem1, out var solution1))
                    {
                        solution1 = GroupSizes(problem1);
                        solutions.TryAdd(problem1, solution1);
                    }

                    //Solve for problem 2
                    if (!solutions.TryGetValue(problem2, out var solution2))
                    {
                        solution2 = GroupSizes(problem2);
                        solutions.TryAdd(problem2, solution2);
                    }

                    // Join solutions for problem 1 and problem 2
                    solution = SumDicts(solution, solution1);
                    solution = SumDicts(solution, solution2);
                    solution[problem1.Polymer[0]]--; // this was counted twice in this iteration
                    solution[problem2.Polymer[0]]--; // this was counted twice in this iteration
                }
                else
                { // This never happens if input is correct
                    solution = SumDicts(solution, CreateDictFromPolymer(polymer));
                }
            }
            solution[problem.Polymer[0]]++; // this was counted one too few times

            return solution;
        }

        private static Dictionary<char, long> SumDicts(Dictionary<char, long> dict1, Dictionary<char, long> dict2)
        {
            Dictionary<char, long> solution = new();
            foreach (var entry in dict1)
                AddToDict(solution, entry.Key, entry.Value);
            foreach (var entry in dict2)
                AddToDict(solution, entry.Key, entry.Value);
            return solution;
        }

        private static void AddToDict(Dictionary<char, long> dict, char c, long x)
        {
            if (!dict.TryAdd(c, x))
                dict[c] += x;
        }

        private static Dictionary<char, long> CreateDictFromPolymer(string polymer)
        {
            Dictionary<char, long> solution = new();
            foreach (var c in polymer)
                AddToDict(solution, c, 1);
            return solution;
        }
    }
}