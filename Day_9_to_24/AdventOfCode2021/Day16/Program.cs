using System;
using System.IO;

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
                Console.WriteLine(packet);
                ParsePacketFromStart(packet);
            }
            Console.WriteLine($"Sum of the packet versions: {versionSum}");
        }

        private static void ParsePacketFromStart(string packet)
        {
            int start = 0;
            ParsePacket(packet, ref start);
            // start is now at position of the first tailing 0
        }

        private static void ParsePacket(string binStr, ref int start)
        {
            Console.WriteLine($"ParsePacket, {binStr}, start: {start}");
            var header = ParseHeader(binStr, ref start);
            int packetVersion = header.Item1;
            int typeId = header.Item2;

            if (typeId == 4)
            {
                ParseLiteralValuePayload(binStr, ref start);
            }
            else
            {
                int typeLengthId = ParseTypeLengthId(binStr, ref start);
                if (typeLengthId == 0)
                {
                    int totalLength = ParseBinNumber(binStr, 15, ref start);
                    int firstStart = start;
                    while (firstStart + totalLength > start)
                    {
                        ParsePacket(binStr, ref start);
                        if (start > firstStart + totalLength)
                            throw new ApplicationException($"start: {start}, firstStart: {firstStart}, totalLength: {totalLength}");
                    }
                }
                else if (typeLengthId == 1)
                {
                    int numberOfPackets = ParseBinNumber(binStr, 11, ref start);
                    for (int i = 0; i < numberOfPackets; i++)
                        ParsePacket(binStr, ref start);
                }
                else
                    throw new ApplicationException($"Invalid type length id found: {typeLengthId}");
            }
        }

        private static void ParseLiteralValuePayload(string binStr, ref int start)
        {
            bool lastGroup = false;
            while (!lastGroup)
            {
                if (binStr[start] == '0')
                    lastGroup = true;

                start++;
                int groupValue = Convert.ToInt32(binStr.Substring(start, 4), 2);
                start += 4;
                // do something with groupValue
            }
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
                int value = Convert.ToInt32(hexStr[i].ToString(), 16);
                string binPart = Convert.ToString(value, 2).PadLeft(4, '0');
                binStr += binPart;
            }

            return binStr;
        }
    }
}