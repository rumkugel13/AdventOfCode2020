using System;
using System.IO;

namespace Day09
{
    class Program
    {
        private static long[] numbers;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            numbers = new long[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                numbers[i] = long.Parse(lines[i]);
            }

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            const int preamble = 25;
            long result = 0;

            for (int i = preamble; i < numbers.Length; i++)
            {
                bool isInList = false;
                for (int j = i - preamble; j < i; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        if (numbers[j] + numbers[k] == numbers[i])
                        {
                            isInList = true;
                            break;
                        }
                    }

                    if (isInList)
                    {
                        break;
                    }
                }

                if (!isInList)
                {
                    // found invalid number
                    result = numbers[i];
                }
            }

            Console.WriteLine("Solution: " + result);
            // Solution: 10884537
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            const long target = 10884537;
            int start = 0, end = 0;
            bool found = false;

            for (int i = 0; i < numbers.Length; i++)
            {
                long targetSum = numbers[i];
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    targetSum += numbers[j];
                    if (targetSum == target)
                    {
                        found = true;
                        start = i;
                        end = j;
                        break;
                    }
                    else if (targetSum > target)
                    {
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            long min = long.MaxValue, max = long.MinValue;

            for (int i = start; i < end; i++)
            {
                min = Math.Min(min, numbers[i]);
                max = Math.Max(max, numbers[i]);
            }

            Console.WriteLine("Solution: " + (min + max));
            // Solution: 1261309
        }
    }
}
