using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        private static string[] lines;
        private static int[] numbers;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");

            // sample
            //lines = new string[] { "0,3,6" };

            var temp = lines[0].Split(',');
            numbers = new int[temp.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = int.Parse(temp[i]);
            }

            SolvePart1();
            //SolvePart2(); // ~7 seconds
            SolvePart2Faster(); // ~1.5 seconds
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");

            Dictionary<int, int> numByTurn = new Dictionary<int, int>();

            const int turns = 2020;
            int turn = 1;
            for (int i = 0; i < numbers.Length; i++)
            {
                numByTurn[turn++] = numbers[i];
            }

            while (turn <= turns)
            {
                int lastNum = numByTurn[turn - 1];
                int number = 0;

                if (numByTurn.Values.Count(x => x == lastNum) > 1)
                {
                    int lastTurn = 0;

                    foreach (var num in numByTurn)
                    {
                        if (num.Value == lastNum && num.Key > lastTurn && num.Key < turn - 1)
                        {
                            lastTurn = num.Key;
                        }
                    }

                    number = turn - 1 - lastTurn;
                }

                numByTurn[turn++] = number;
            }

            Console.WriteLine("Solution: " + numByTurn[turns]);
            // Solution: 929
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            const int turns = 30000000;

            Dictionary<int, int> turnByNum = new Dictionary<int, int>();
            Dictionary<int, int> numCount = new Dictionary<int, int>();

            int turn = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                turnByNum[numbers[i]] = turn++;
                numCount.Add(numbers[i], 1);
            }

            int lastNum = numbers[numbers.Length - 1];

            while (turn < turns)
            {
                int number = 0;

                if (numCount[lastNum] > 1)
                {
                    number = turn - 1 - turnByNum[lastNum];
                }

                turnByNum[lastNum] = turn - 1;
                lastNum = number;

                if (numCount.ContainsKey(lastNum))
                    numCount[lastNum]++;
                else
                    numCount[lastNum] = 1;

                turn++;
            }

            Console.WriteLine("Solution: " + lastNum);
            // Solution: 16671510
        }

        private static void SolvePart2Faster()
        {
            Console.WriteLine("Solving Part 2 faster");

            const int turns = 30000000;

            int[] turnByNum = new int[turns];

            int turn = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                turnByNum[numbers[i]] = 1 + turn++;
            }

            int lastNum = numbers[numbers.Length - 1];

            for (; turn < turns; turn++)
            {
                int number = turnByNum[lastNum] != 0 ? turn - turnByNum[lastNum] : 0;

                turnByNum[lastNum] = turn;
                lastNum = number;
            }

            Console.WriteLine("Solution: " + lastNum);
            // Solution: 16671510
        }
    }
}
