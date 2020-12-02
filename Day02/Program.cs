using System;
using System.Collections.Generic;
using System.IO;

namespace Day02
{
    class Program
    {
        static List<string> lines;

        static void Main(string[] args)
        {
            lines = new List<string>();

            using (StreamReader read = new StreamReader(new FileStream("input.txt", FileMode.Open)))
            {
                while (!read.EndOfStream)
                {
                    lines.Add(read.ReadLine());
                }
            }

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            int correct = 0;

            foreach (string line in lines)
            {
                string[] data = line.Split(':');
                string[] policy = data[0].Split(' ');
                string[] numbers = policy[0].Split('-');
                string character = policy[1];
                string password = data[1].Trim();
                int count = 0;

                foreach (char c in password)
                {
                    if (c.ToString().Equals(character))
                    {
                        count++;
                    }
                }

                if (count >= Convert.ToInt32(numbers[0]) && count <= Convert.ToInt32(numbers[1]))
                {
                    correct++;
                }
            }

            Console.WriteLine("Solution: " + correct);
            // Solution: 524
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            int correct = 0;

            foreach (string line in lines)
            {
                string[] data = line.Split(':');
                string[] policy = data[0].Split(' ');
                string[] numbers = policy[0].Split('-');
                string character = policy[1];
                string password = data[1].Trim();
                bool one = false;
                
                if (password[Convert.ToInt32(numbers[0]) - 1].ToString().Equals(character))
                {
                    correct++;
                    one = true;
                }

                if (password[Convert.ToInt32(numbers[1]) - 1].ToString().Equals(character))
                {
                    if (one) // already counted once, cant be both position
                    {
                        correct--;
                    }
                    else
                    {
                        correct++;
                    }
                }
            }

            Console.WriteLine("Solution: " + correct);
            // Solution: 485
        }
    }
}
