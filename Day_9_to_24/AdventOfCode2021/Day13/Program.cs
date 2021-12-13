using System;
using System.IO;

namespace Day13
{
    internal class Program
    {
        private static void Main()
        {
            // Read Input
            string[] lines = File.ReadAllLines("input.txt");
            var widthAndHeight = FindWidthAndHeight(lines);
            Paper paper = new(widthAndHeight.Item1, widthAndHeight.Item2);
            int i;
            for (i = 0; lines[i] != ""; i++)
            {
                string[] words = lines[i].Split(',');
                // add dots to paper
                paper[Convert.ToInt32(words[0]), Convert.ToInt32(words[1])] = true;
            }

            // Solution 1
            paper.Fold(ParseInstruction(lines[++i]));
            Console.WriteLine($"Number of dots after 1 fold: {paper.NumberOfDots()}");

            // Solution 2
            for (i++; i < lines.Length; i++)
            {
                paper.Fold(ParseInstruction(lines[i]));
            }
            paper.PrintPaper();
        }

        private static Tuple<int, int> FindWidthAndHeight(string[] lines)
        {
            int maxWidth = 0;
            int maxHeight = 0;
            for (int i = 0; lines[i] != ""; i++)
            {
                string[] words = lines[i].Split(',');
                maxWidth = Math.Max(maxWidth, Convert.ToInt32(words[0]));
                maxHeight = Math.Max(maxHeight, Convert.ToInt32(words[1]));
            }
            return new(maxWidth + 1, maxHeight + 1);
        }

        private static string ParseInstruction(string line)
        {
            string[] words = line.Split();
            return words[^1];
        }
    }

    internal class Paper
    {
        private int width;
        private int height;
        private bool[,] paper;

        public Paper(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.paper = new bool[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    this.paper[x, y] = false;
        }

        public bool this[int x, int y]
        {
            get => paper[x, y];
            set => paper[x, y] = value;
        }

        public void Fold(string instruction)
        {
            int pos = Convert.ToInt32(instruction[2..]);
            if (instruction.StartsWith("x="))
            {
                int numberOfColumns = Math.Min(pos, width - pos);
                for (int xDiff = 1; xDiff <= numberOfColumns; xDiff++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        paper[pos - xDiff, y] = paper[pos - xDiff, y] || paper[pos + xDiff, y];
                    }
                }
                width = numberOfColumns;
            }
            else if (instruction.StartsWith("y="))
            {
                int numberOfRows = Math.Min(pos, height - pos);
                for (int yDiff = 1; yDiff <= numberOfRows; yDiff++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        paper[x, pos - yDiff] = paper[x, pos - yDiff] || paper[x, pos + yDiff];
                    }
                }
                height = numberOfRows;
            }
            else throw new ApplicationException("Some mistake happened in parsing.");
        }

        public int NumberOfDots()
        {
            int count = 0;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (paper[x, y])
                        count++;
            return count;
        }

        public void PrintPaper()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    if (paper[x, y])
                        Console.Write("#");
                    else Console.Write(".");
                Console.WriteLine();
            }
        }
    }
}