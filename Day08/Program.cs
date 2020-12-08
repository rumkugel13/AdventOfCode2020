using System;
using System.Collections.Generic;
using System.IO;

namespace Day08
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
            Compute(lines, out int accumulator);

            Console.WriteLine("Solution: " + accumulator);
            // Solution: 1600
        }

        private static bool Compute(string[] instructions, out int accumulator)
        {
            accumulator = 0;
            int ic = 0;
            HashSet<int> computedLines = new HashSet<int>();

            while (!computedLines.Contains(ic) && ic < instructions.Length)
            {
                string instruction = lines[ic].Split(' ')[0];
                int number = int.Parse(lines[ic].Split(' ')[1]);
                computedLines.Add(ic);

                switch (instruction)
                {
                    case "nop":
                        ic++;
                        break;
                    case "acc":
                        accumulator += number;
                        ic++;
                        break;
                    case "jmp":
                        ic += number;
                        break;
                }
            }

            return computedLines.Contains(ic);
        }

        static bool TestSwitch(int i, string from, string to, out int accumulator)
        {
            lines[i] = lines[i].Replace(from, to);
            if (!Compute(lines, out accumulator))
            {
                return true;
            }
            // switch back
            lines[i] = lines[i].Replace(to, from);
            return false;
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            int result = 0;

            for (int i = 0; i < lines.Length; i++)
            {                
                if (lines[i].StartsWith("nop") && TestSwitch(i, "nop", "jmp", out int accumulator))
                {
                    result = accumulator;
                    break;
                }
                else if (lines[i].StartsWith("jmp") && TestSwitch(i, "jmp", "nop", out int accumulator2))
                {
                    result = accumulator2;
                    break;
                }
            }

            Console.WriteLine("Solution: " + result);
            // Solution: 1543
        }

    }
}
