using System;
using System.IO;

namespace Day03
{
    class Program
    {
        static string[] lines;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");

            int trees = TreeCount(3, 1);

            Console.WriteLine("Solution: " + trees);
            // Solution: 216
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            long treesMultiplied = TreeCount(1, 1);
            treesMultiplied *= TreeCount(3, 1);
            treesMultiplied *= TreeCount(5, 1);
            treesMultiplied *= TreeCount(7, 1);
            treesMultiplied *= TreeCount(1, 2);

            Console.WriteLine("Solution: " + treesMultiplied);
            // Solution: -
        }

        private static int TreeCount(int width, int height)
        {
            int linewidth = lines[0].Length;
            int trees = 0;

            for (int i = 0, column = 0; i < lines.Length; i += height, column += width)
            {
                if (lines[i][column % linewidth].Equals('#'))
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}
