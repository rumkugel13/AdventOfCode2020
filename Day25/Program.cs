using System;
using System.IO;

namespace Day25
{
    internal class Program
    {
        static long[] pubKeys;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var numbers = File.ReadAllLines("input.txt");
            pubKeys = new long[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                pubKeys[i] = long.Parse(numbers[i]);
            }

            SolvePart1();
        }

        private static void SolvePart1()
        {
            const long subjectNumber = 7;
            const long divisor = 20201227;

            long temp = 1;
            int loopNumber = 0;
            for (; temp != pubKeys[0]; loopNumber++)
            {
                temp *= subjectNumber;
                temp %= divisor;
            }

            long result = 1;
            for (int i = 0; i < loopNumber; i++)
            {
                result *= pubKeys[1];
                result %= divisor;
            }

            // 14205034 too high
            Console.WriteLine(result);
            //Console.WriteLine("Solution: " + result);
            // Solution: 297257
        }
    }
}
