using System;
using System.IO;

namespace Day05
{
    class Program
    {
        static string[] lines;
        static int[] ids;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");
            ids = new int[lines.Length];

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            int max = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                int row = GetRow(lines[i].Substring(0, 7));
                int column = GetColumn(lines[i].Substring(7, 3));
                int id = row * 8 + column;
                ids[i] = id;
                max = Math.Max(id, max);
            }

            Console.WriteLine("Solution: " + max);
            // Solution: 933
        }

        private static int GetRow(string data)
        {
            string binary = data.Replace('F', '0').Replace('B', '1');
            return Convert.ToInt32(binary, 2);
        }

        private static int GetColumn(string data)
        {
            string binary = data.Replace('L', '0').Replace('R', '1');
            return Convert.ToInt32(binary, 2);
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            int result = 0;

            Array.Sort(ids);

            for (int i = 1; i < ids.Length - 1; i++)
            {
                if (ids[i] -1 != ids [i-1])
                {
                    result = ids[i] - 1;
                    break;
                }
            }

            Console.WriteLine("Solution: " + result);
            // Solution: 711
        }
    }
}
