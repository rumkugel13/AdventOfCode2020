using System;
using System.Collections.Generic;
using System.IO;

namespace Day06
{
    class Program
    {
        private static string[] lines;
        private static List<string> items = new List<string>();

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");
            string buffer = "";

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    items.Add(buffer.Trim());
                    buffer = "";
                    continue;
                }

                buffer += lines[i];
            }

            // don't forget last one
            items.Add(buffer.Trim());

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            HashSet<char> characters = new HashSet<char>();
            int count = 0;

            foreach (string item in items)
            {
                foreach (char c in item)
                {
                    characters.Add(c);
                }

                count += characters.Count;
                characters.Clear();
            }

            Console.WriteLine("Solution: " + count);
            // Solution: 6583
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            Dictionary<char, int> yesCount = new Dictionary<char, int>();
            int peopleCount = 0;
            int totalYes = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    peopleCount++;
                    // one group
                    foreach (char c in lines[i])
                    {
                        if (!yesCount.ContainsKey(c))
                        {
                            yesCount[c] = 1;
                        }
                        else
                        {
                            yesCount[c] += 1;
                        }
                    }
                }
                else
                {
                    foreach (int n in yesCount.Values)
                    {
                        if (n == peopleCount)
                        {
                            totalYes++;
                        }
                    }

                    // switch group
                    yesCount.Clear();
                    peopleCount = 0;
                }
            }

            // count last one
            foreach (int n in yesCount.Values)
            {
                if (n == peopleCount)
                {
                    totalYes++;
                }
            }

            Console.WriteLine("Solution: " + totalYes);
            // Solution: 3290
        }
    }
}
