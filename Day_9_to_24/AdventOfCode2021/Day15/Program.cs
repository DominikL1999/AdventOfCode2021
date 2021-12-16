using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    internal class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);
        }

        public bool Equals(Position other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    internal class UpdateTracker
    {
        private readonly bool[,] toUpdateLookupTable;
        private readonly Stack<Position> toUpdateStack;

        public UpdateTracker(int width, int height)
        {
            toUpdateLookupTable = new bool[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    toUpdateLookupTable[x, y] = false;
            toUpdateStack = new();
        }

        public bool NeedsUpdate(Position pos)
        {
            return toUpdateLookupTable[pos.X, pos.Y];
        }

        public bool HasAnotherUpdate()
        {
            return toUpdateStack.Count > 0;
        }

        public Position NextUpdate()
        {
            if (!HasAnotherUpdate()) return null;

            var nextUpdate = toUpdateStack.Pop();
            if (toUpdateLookupTable[nextUpdate.X, nextUpdate.Y] == false)
                throw new Exception("The update is not in the lookup table --> Inconsistency");

            toUpdateLookupTable[nextUpdate.X, nextUpdate.Y] = false;

            return nextUpdate;
        }

        public bool UpdatePending(Position pos)
        {
            return toUpdateLookupTable[pos.X, pos.Y];
        }

        public bool SaveUpdate(Position pos)
        {
            if (toUpdateLookupTable[pos.X, pos.Y] == true) return false;
            else
            {
                toUpdateLookupTable[pos.X, pos.Y] = true;
                toUpdateStack.Push(pos);
                return true;
            }
        }

        public int NumberOfUpdatesLeft()
        {
            return toUpdateStack.Count;
        }
    }

    internal class Program
    {
        private static UpdateTracker updateTracker;

        private static void Main()
        {
            string[] lines = File.ReadAllLines("input.txt");

            //int width = lines[0].Length;
            //int height = lines.Length;
            //int[,] map = new int[width, height];
            //int[,] riskLevels = new int[width, height];

            // Solution 1
            //for (int x = 0; x < width; x++)
            //    for (int y = 0; y < height; y++)
            //    {
            //        map[x, y] = int.Parse(lines[x][y].ToString());
            //        riskLevels[x, y] = int.MaxValue;
            //    }
            //riskLevels[0, 0] = 0;

            //UpdateRiskLevels(riskLevels, map, 0, 0);
            //Console.WriteLine($"Minimum risk level: {riskLevels[width - 1, height - 1]}");

            // Solution 2

            const int SIZE = 5;
            int width = lines[0].Length * SIZE;
            int height = lines.Length * SIZE;
            int[,] map = new int[width, height];
            int[,] riskLevels = new int[width, height];

            // todo: refactor this
            int[,] mapSimple = new int[lines[0].Length, lines.Length];
            for (int x = 0; x < lines[0].Length; x++)
                for (int y = 0; y < lines.Length; y++)
                    mapSimple[x, y] = int.Parse(lines[x][y].ToString());

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    for (int x = 0; x < lines[0].Length; x++)
                    {
                        for (int y = 0; y < lines.Length; y++)
                        {
                            int x_ = i * lines[0].Length + x;
                            int y_ = j * lines.Length + y;
                            map[x_, y_] = i + j + mapSimple[x, y];
                            if (map[x_, y_] > 9)
                                map[x_, y_] -= 9;
                        }
                    }
                }
            }
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    riskLevels[x, y] = int.MaxValue;
            riskLevels[0, 0] = 0;

            updateTracker = new(width, height);
            updateTracker.SaveUpdate(new(0, 0));

            Position nextUpdate;
            int count = 0;
            while ((nextUpdate = updateTracker.NextUpdate()) != null)
            {
                count++;
                if (count % 1000000 == 0)
                {
                    //int numberOfNonInfinites = NumberOfNonInfinites(riskLevels, width, height);
                    Console.WriteLine($"# updates: {updateTracker.NumberOfUpdatesLeft()}, #non-inf: {NumberOfNonInfinites(riskLevels, width, height)}");
                }
                UpdateRiskLevels(riskLevels, map, nextUpdate.X, nextUpdate.Y);
            }

            Console.WriteLine($"Solution 2: {riskLevels[width - 1, height - 1]}");
        }

        private static int NumberOfNonInfinites(int[,] riskLevels, int width, int height)
        {
            int count = 0;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (riskLevels[x, y] != int.MaxValue)
                        count++;
            return count;
        }

        private static void PrintBoard(int[,] board, string name)
        {
            int width = board.GetLength(0);
            int height = board.GetLength(1);
            Console.WriteLine($"================ {name} ({width} x {height}) ================ ");
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Console.Write($"{board[x, y]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("================================================");
        }

        private static void UpdateRiskLevels(int[,] riskLevels, int[,] map, int x, int y)
        {
            int width = riskLevels.GetLength(0);
            int height = riskLevels.GetLength(1);
            var neighbours = ValidNeighbours(x, y, width, height);

            // set this to the minimum of the neighbouring nodes + the
            // value at map[x, y] if necessary
            int min = riskLevels[x, y];
            foreach (var neighbour in neighbours)
            {
                min = Math.Min(min, riskLevels[neighbour.X, neighbour.Y]);
            }
            if (min < riskLevels[x, y])
            { // an update at riskLevels[x, y] needs to happen
                riskLevels[x, y] = min + map[x, y];
            }
            foreach (var neighbour in neighbours)
            {
                // update neighbours if necessary
                if (riskLevels[x, y] + map[neighbour.X, neighbour.Y] < riskLevels[neighbour.X, neighbour.Y])
                {
                    riskLevels[neighbour.X, neighbour.Y] = riskLevels[x, y] + map[neighbour.X, neighbour.Y];
                    if (!updateTracker.UpdatePending(neighbour))
                        updateTracker.SaveUpdate(neighbour);
                }
            }
        }

        private static List<Position> ValidNeighbours(int x, int y, int width, int height)
        {
            List<Position> allNeighbours = new()
            {
                new(x - 1, y),
                new(x, y - 1),
                new(x + 1, y),
                new(x, y + 1)
            };
            var validNeighbours = allNeighbours.Where(p => IsValidPosition(p.X, p.Y, width, height)).ToList();
            return validNeighbours;
        }

        private static bool IsValidPosition(int x, int y, int width, int height)
        {
            return (x >= 0 && x < width && y >= 0 && y < height);
        }
    }
}