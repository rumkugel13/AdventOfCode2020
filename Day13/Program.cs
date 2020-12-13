using System;
using System.IO;

namespace Day13
{
    class Program
    {
        private static string[] lines;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");

            int earliest = int.Parse(lines[0]);
            var buses = lines[1].Split(',');

            long earliestBus = 0;
            long waitTime = long.MaxValue;

            foreach (var bus in buses)
            {
                if (long.TryParse(bus, out long id))
                {
                    if (GetWaitTime(earliest, id) < waitTime)
                    {
                        earliestBus = id;
                        waitTime = GetWaitTime(earliest, id);
                    }
                }
            }

            Console.WriteLine("Solution: " + (earliestBus * waitTime));
            // Solution: 2545
        }

        private static long GetWaitTime(long timestamp, long id)
        {
            return id - (timestamp % id);
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            var temp = lines[1].Split(',');

            //// sample data with solution 1068781
            //temp = new string[] { "7", "13", "x", "x", "59", "x", "31", "19" };
            //// sample data with solution 3417
            //temp = new string[] { "17", "x", "13", "19" };
            //// sample data with solution 754018
            //temp = new string[] { "67", "7", "59", "61" };
            //// sample data with solution 779210
            //temp = new string[] { "67", "x", "7", "59", "61" };
            //// sample data with solution 1261476
            //temp = new string[] { "67", "7", "x", "59", "61" };
            //// sample data with solution 1202161486
            //temp = new string[] { "1789", "37", "47", "1889" };

            var buses = new long[temp.Length];
            for (int i = 0; i < buses.Length; i++)
            {
                if (long.TryParse(temp[i], out long id))
                {
                    buses[i] = id;
                }
            }

            long timestamp = 0;
            long increment = 1;
            
            for (int i = 0; i < buses.Length; i++)
            {
                if (buses[i] == 0)
                    continue;

                while ((timestamp + i) % buses[i] != 0)
                {
                    timestamp += increment;
                }

                increment *= buses[i];
            }

            Console.WriteLine("Solution: " + timestamp);
            // Solution: 266204454441577
        }
    }
}
