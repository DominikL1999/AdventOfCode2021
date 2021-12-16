using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    internal class Program
    {
        private static int versionSum = 0;

        private static void Main()
        {
            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                string packet = HexToBin(line);
                long value = ParsePacketFromStart(packet);
                Console.WriteLine($"{packet} -> {value}");
            }
            Console.WriteLine($"Sum of the packet versions: {versionSum}");
        }

        private static long ParsePacketFromStart(string packet)
        {
            int start = 0;
            return ParsePacket(packet, ref start);
            // start is now at position of the first tailing 0
        }

        private static long ParsePacket(string binStr, ref int start)
        {
            var header = ParseHeader(binStr, ref start);
            int typeId = header.Item2;

            if (typeId == 4)
            {
                return ParseLiteralValuePayload(binStr, ref start);
            }
            else
            {
                List<long> packetValues = new();
                int typeLengthId = ParseTypeLengthId(binStr, ref start);
                if (typeLengthId == 0)
                {
                    long totalLength = ParseBinNumber(binStr, 15, ref start);
                    int firstStart = start;
                    while (firstStart + totalLength > start)
                    {
                        long value = ParsePacket(binStr, ref start);
                        packetValues.Add(value);
                        if (start > firstStart + totalLength)
                            throw new ApplicationException($"start: {start}, firstStart: {firstStart}, totalLength: {totalLength}");
                    }
                }
                else if (typeLengthId == 1)
                {
                    int numberOfPackets = ParseBinNumber(binStr, 11, ref start);
                    for (int i = 0; i < numberOfPackets; i++)
                    {
                        long value = ParsePacket(binStr, ref start);
                        packetValues.Add(value);
                    }
                }
                else
                    throw new ApplicationException($"Invalid type length id found: {typeLengthId}");

                // do something using the values found in the packets
                // corrsponding to the type id
                return GetPacketValue(typeId, packetValues);
            }
        }

        private static long GetPacketValue(long typeId, List<long> packetValues)
        {
            if (typeId == 0)
                return packetValues.Sum();
            else if (typeId == 1)
                return packetValues.Aggregate((a, b) => a * b);
            else if (typeId == 2)
                return packetValues.Min();
            else if (typeId == 3)
                return packetValues.Max();
            else if (typeId == 5)
                return Convert.ToInt64(packetValues[0] > packetValues[1]);
            else if (typeId == 6)
                return Convert.ToInt64(packetValues[0] < packetValues[1]);
            else if (typeId == 7)
                return Convert.ToInt64(packetValues[0] == packetValues[1]);
            else
                throw new ApplicationException("D:");
        }

        private static long ParseLiteralValuePayload(string binStr, ref int start)
        {
            bool lastGroup = false;
            long value = 0;
            while (!lastGroup)
            {
                if (binStr[start] == '0')
                    lastGroup = true;

                start++;
                long groupValue = Convert.ToInt64(binStr.Substring(start, 4), 2);
                value *= 16;
                value += groupValue;
                start += 4;
            }

            return value;
        }

        private static Tuple<int, int> ParseHeader(string binStr, ref int start)
        {
            int packetVersion = Convert.ToInt32(binStr.Substring(start, 3), 2);
            versionSum += packetVersion;
            start += 3;
            int typeId = Convert.ToInt32(binStr.Substring(start, 3), 2);
            start += 3;
            return new(packetVersion, typeId);
        }

        private static int ParseTypeLengthId(string binStr, ref int start)
        {
            return ParseBinNumber(binStr, 1, ref start);
        }

        private static int ParseBinNumber(string binStr, int length, ref int start)
        {
            int number = Convert.ToInt32(binStr.Substring(start, length), 2);
            start += length;
            return number;
        }

        private static string HexToBin(string hexStr)
        {
            string binStr = "";

            for (int i = 0; i < hexStr.Length; i++)
            {
                long value = Convert.ToInt64(hexStr[i].ToString(), 16);
                string binPart = Convert.ToString(value, 2).PadLeft(4, '0');
                binStr += binPart;
            }

            return binStr;
        }
    }
}