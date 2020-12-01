using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static List<int> numbers;

        static void Main(string[] args)
        {
            numbers = new List<int>();

            using (StreamReader read = new StreamReader(new FileStream("input.txt", FileMode.Open)))
            {
                while (!read.EndOfStream)
                {
                    numbers.Add(Convert.ToInt32(read.ReadLine()));
                }
            }

            SolvePart1();
            SolvePart2();
        }

        static void SolvePart1()
        {
            Console.WriteLine("Solve Part 1");
            const int sum = 2020;
            int result = 0;

            foreach (int n in numbers)
            {
                foreach (int n2 in numbers)
                {
                    if (n != n2)
                    {
                        if (n + n2 == sum)
                        {
                            result = n * n2;
                            break;
                        }
                    }
                }
                if (result != 0)
                {
                    break;
                }
            }

            Console.WriteLine("Result: " + result);
            // Result: 197451
        }

        static void SolvePart2()
        {
            Console.WriteLine("Solve Part 2");
            const int sum = 2020;
            int result = 0;

            foreach (int n in numbers)
            {
                foreach (int n2 in numbers)
                {
                    foreach (int n3 in numbers)
                    {
                        if (n != n3 && n!=n2 && n2!=n3)
                        {
                            if (n + n2 + n3 == sum)
                            {
                                result = n * n2 * n3;
                                break;
                            }
                        }
                    }
                    if (result != 0)
                    {
                        break;
                    }
                }
                if (result != 0)
                {
                    break;
                }
            }

            Console.WriteLine("Result: " + result);
            // Result: 138233720
        }
    }
}
