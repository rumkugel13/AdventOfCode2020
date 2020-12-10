using System;
using System.Collections.Generic;
using System.IO;

namespace Day10
{
    class Program
    {
        private static int[] numbers;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            numbers = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                numbers[i] = int.Parse(lines[i]);
            }

            // first test numbers
            //numbers = new int[] { 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4 };

            // second test numbers
            //numbers = new int[] { 28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3 };

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            Array.Sort(numbers);
            int diff1 = 0, diff2 = 0, diff3 = 0;

            // outlet is 0, diff is to lowest adapter in bag
            if (numbers[0] == 1)
            {
                diff1++;
            }
            else if (numbers[0] == 2)
            {
                diff2++;
            }
            else if (numbers[0] == 3)
            {
                diff3++;
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int diff = numbers[i + 1] - numbers[i];
                if (diff == 1)
                {
                    diff1++;
                }
                else if (diff == 2)
                {
                    diff2++;
                }
                else if (diff == 3)
                {
                    diff3++;
                }
            }

            // devices adapter is 3 higher than highest adapter in bag
            diff3++;

            Console.WriteLine("Solution: " + (diff1 * diff3));
            // Solution: 2664
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            //Dictionary<int, long> counted = new Dictionary<int, long>();
            //counted[numbers.Length - 1] = 1; // last can only have one arrangement

            long result = CountArrangements();

            Console.WriteLine("Solution: " + result);
            // Solution: 148098383347712
        }

        static long CountArrangements()
        {
            Dictionary<int, long> counted = new Dictionary<int, long>();
            counted[numbers.Length - 1] = 1; // last can only have one arrangement

            for (int index = numbers.Length - 2; index >= 0; index--)
            {
                long arrangements = 0;
                for (int i = index + 1; i < numbers.Length && numbers[i] - numbers[index] <= 3; i++)
                {
                    //if (((index + i) < numbers.Length) && (numbers[index + i] - numbers[index] <= 3))
                    {
                        // valid next number
                        arrangements += counted[i];
                    }
                }
                counted[index] = arrangements;
            }

            // note: any three of those could be the starting one if diff to 0 <= 3
            return counted[0] + (numbers[1] <= 3 ? counted[1] : 0) + (numbers[2] <= 3 ? counted[2] : 0);
        }

        //static long CountArrangements(Dictionary<int, long> alreadyCounted, int index)
        //{
        //    if (alreadyCounted.ContainsKey(index)) 
        //        return alreadyCounted[index];

        //    long arrangements = 0;
        //    for (int i = 1; i <= 3; i++)
        //    {
        //        if (((index + i) < numbers.Length) && (numbers[index + i] - numbers[index] <= 3))
        //        {
        //            // valid next number
        //            arrangements += CountArrangements(alreadyCounted, index + i);
        //        }
        //    }
        //    alreadyCounted[index] = arrangements;

        //    return arrangements;
        //}
    }
}
