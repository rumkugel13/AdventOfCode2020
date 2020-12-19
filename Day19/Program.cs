using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Day19
{
    class Program
    {
        private static string[] lines;
        private static Dictionary<int, string> rules;
        private static int startindex = 0;
        private static int depth11 = 0;
        private static int depth8 = 0;

        static void Main(string[] args)
        {
            lines = System.IO.File.ReadAllLines("input.txt");
            rules = new Dictionary<int, string>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    startindex = i + 1;
                    break;
                }

                var parts = lines[i].Split(':');
                rules.Add(int.Parse(parts[0]), parts[1].Trim());
            }

            SolvePart1();
            SolvePart2();
        }

        private static string Traverse(int index)
        {
            StringBuilder builder = new StringBuilder();

            var rule = rules[index];

            // for part2
            if (index == 8 && rule.Contains('|'))
            {
                // "42 | 42 8"
                depth8++;

                if (depth8 > 5)
                {
                    rule = "42";
                }
            }
            else if (index == 11 && rule.Contains('|'))
            {
                // "42 31 | 42 11 31"
                depth11++;

                if (depth11 > 5)
                {
                    rule = "42 31";
                }
            }

            if (rule.Contains('\"'))
            {
                builder.Append(rule.Substring(1, 1));
            }
            else if (rule.Contains('|'))
            {
                builder.Append("((");
                var parts = rules[index].Split('|');

                var nums1 = parts[0].Trim().Split(' ');
                builder.Append(Traverse(int.Parse(nums1[0])));
                if (nums1.Length > 1)
                    builder.Append(Traverse(int.Parse(nums1[1])));

                builder.Append(")|(");

                var nums2 = parts[1].Trim().Split(' ');
                builder.Append(Traverse(int.Parse(nums2[0])));
                if (nums2.Length > 1)
                    builder.Append(Traverse(int.Parse(nums2[1])));
                // part 2: index 11 has 3 numbers
                if (nums2.Length > 2)
                    builder.Append(Traverse(int.Parse(nums2[2])));

                builder.Append("))");
            }
            else
            {
                var nums = rule.Split(' ');
                builder.Append(Traverse(int.Parse(nums[0])));
                if (nums.Length > 1)
                    builder.Append(Traverse(int.Parse(nums[1])));
            }

            return builder.ToString();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            var regex = "^(" + Traverse(0) + ")$";
            int count = 0;

            for (int i = startindex; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], regex))
                {
                    count++;
                }
            }

            Console.WriteLine("Solution: " + count);
            // Solution: 265
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

            var regex = "^(" + Traverse(0) + ")$";
            int count = 0;

            for (int i = startindex; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], regex))
                {
                    count++;
                }
            }

            Console.WriteLine("Solution: " + count);
            // Solution: 394
        }
    }
}
