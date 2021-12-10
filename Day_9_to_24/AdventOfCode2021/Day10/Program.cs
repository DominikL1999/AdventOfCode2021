using System;
using System.Collections.Generic;
using System.IO;

namespace Day10
{
    internal class Program
    {
        private static readonly Dictionary<char, char> correspondingClosedBracket = new()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }
        };

        private static readonly Dictionary<char, int> syntaxErrorScore = new()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        private static readonly Dictionary<char, long> bracketWeighting = new()
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 }
        };

        private static void Main()
        {
            using var streamReader = new StreamReader("input.txt");
            string line;
            int errorSum = 0;
            List<long> completionScores = new();
            while ((line = streamReader.ReadLine()) != null)
            {
                if (IsCorrupt(line, out int position, out Stack<char> openBrackets))
                    // Part 1
                    errorSum += syntaxErrorScore[line[position]];
                else // Part 2
                    completionScores.Add(CompletionScore(openBrackets));
            }
            completionScores.Sort();
            long middleValue = completionScores[(completionScores.Count - 1) / 2];
            Console.WriteLine($"Sum of error values: {errorSum}");
            Console.WriteLine($"Middle error value: {middleValue}");
        }

        private static bool IsCorrupt(string line, out int position, out Stack<char> openBrackets)
        {
            openBrackets = new();
            for (position = 0; position < line.Length; position++)
            {
                char current = line[position];
                if (correspondingClosedBracket.ContainsKey(current)) // is opening bracket
                    openBrackets.Push(current);
                else if (correspondingClosedBracket.ContainsValue(current)) // is closing bracket
                {
                    if (correspondingClosedBracket[openBrackets.Peek()] != current)
                        return true;
                    else
                        openBrackets.Pop();
                }
                else throw new Exception("Invalid character");
            }
            return false;
        }

        private static long CompletionScore(Stack<char> openBrackets)
        {
            long sum = 0;
            foreach (var bracket in openBrackets)
            {
                long weighting = bracketWeighting[correspondingClosedBracket[bracket]];
                sum *= 5;
                sum += weighting;
            }
            return sum;
        }
    }
}