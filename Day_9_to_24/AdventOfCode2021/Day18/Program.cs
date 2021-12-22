using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities;

namespace Day18
{
    using SnailNumber = List<int>;

    internal class Program
    {
        private static void Main()
        {
            // Read input
            string[] lines = File.ReadAllLines("input.txt");
            List<SnailNumber> snailNumbers = lines.Select(
                line => line.Select(
                    c => GetDigit(c)).ToList()).ToList();

            SnailNumber solution = snailNumbers.Aggregate((n1, n2) =>
            {
                ReduceSnailNumber(n1);
                ReduceSnailNumber(n2);

                SnailNumber n3 = AddSnailNumbers(n1, n2);
                ReduceSnailNumber(n3);

                return n3;
            });

            Console.WriteLine($"solution: ");
            PrintSnailNumber(solution);
            Console.WriteLine($"Magnitude: {Magnitude(solution)}");
        }

        private static SnailNumber AddSnailNumbers(SnailNumber n1, SnailNumber n2)
        {
            SnailNumber result = new();
            result.Add(GetDigit('['));
            result.AddRange(n1);
            result.Add(GetDigit(','));
            result.AddRange(n2);
            result.Add(GetDigit(']'));
            return result;
        }

        private static SnailNumber CreatePair(int leftPart, int rightPart)
        {
            return AddSnailNumbers(new() { leftPart }, new() { rightPart });
        }

        private static void ReduceSnailNumber(SnailNumber number)
        {
            while (Algorithm.TryOperations(number, new() { TryExplode, TrySplit }))
            {
            }
        }

        private static bool TryExplode(SnailNumber number)
        {
            int depth = 0;
            for (int i = 0; i < number.Count; i++)
            {
                if (number[i] == GetDigit('[')) // todo: refactor
                    depth++;
                else if (number[i] == GetDigit(']'))
                    depth--;
                else
                { // is number
                    if (depth >= 5)
                    {   // according to problem specification this is always a pair
                        // -> explode this number
                        bool stop = false;
                        for (int j = i - 1; j >= 0 && !stop; j--)
                        {
                            if (IsNumber(number[j]))
                            {
                                number[j] += number[i];
                                stop = true;
                            }
                        }
                        stop = false;
                        for (int j = i + 4; j < number.Count && !stop; j++)
                        {
                            if (IsNumber(number[j]))
                            {
                                number[j] += number[i + 2];
                                stop = true;
                            }
                        }

                        // Remove the pair
                        number.RemoveRange(i - 1, 5);

                        // Add a 0 in its place
                        number.Insert(i - 1, 0);

                        return true;
                    }
                }
            }

            return false;
        }

        private static bool TrySplit(SnailNumber number)
        {
            for (int i = 0; i < number.Count; i++)
            {
                if (number[i] >= 10)
                {
                    int leftPart = number[i] / 2;
                    int rightPart = number[i] - leftPart;
                    number.RemoveAt(i);
                    SnailNumber splitPair = CreatePair(leftPart, rightPart);
                    number.InsertRange(i, splitPair);

                    return true;
                }
            }

            return false;
        }

        private static int Magnitude(SnailNumber number)
        {
            while (Algorithm.TryOperations(number, new() { ReplaceLeftmostPair }))
            { }

            return number.Single();
        }

        private static bool ReplaceLeftmostPair(SnailNumber number)
        {
            for (int i = 0; i < number.Count; i++)
            {
                if (number[i] == GetDigit('[') && number[i + 4] == GetDigit(']'))
                {
                    int pairMagnitude = PairMagnitude(number, i);
                    number.RemoveRange(i, 5);
                    number.Insert(i, pairMagnitude);
                    return true;
                }
            }

            return false;
        }

        private static int PairMagnitude(SnailNumber number, int position)
        {
            return 3 * number[position + 1] + 2 * number[position + 3];
        }

        private static int GetDigit(char c)
        {
            if (c == '[') return -1;
            else if (c == ']') return -2;
            else if (c == ',') return -3;
            else return int.Parse(c.ToString());
        }

        private static string GetString(int digit)
        {
            if (digit == -1) return "[";
            else if (digit == -2) return "]";
            else if (digit == -3) return ",";
            else return digit.ToString();
        }

        private static bool IsNumber(int x)
        {
            return x >= 0;
        }

        private static void PrintSnailNumber(SnailNumber number)
        {
            foreach (int x in number)
                Console.Write($"{GetString(x)}");
            Console.WriteLine();
        }
    }
}