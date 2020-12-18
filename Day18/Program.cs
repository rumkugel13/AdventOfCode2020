using System;

namespace Day18
{
    class Program
    {
        private static string[] lines;

        static void Main(string[] args)
        {
            lines = System.IO.File.ReadAllLines("input.txt");
            // sample 
            //lines = System.IO.File.ReadAllLines("sample.txt");

            //lines = new string[] { "2 * 3 + (4 * 5)" };

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            long sum = 0;

            foreach (string line in lines)
            {
                int index = 0;
                sum += Evaluate(line, ref index);
            }

            Console.WriteLine("Solution: " + sum);
            // Solution: 1408133923393
        }

        private static long Evaluate(string line, ref int index)
        {
            long result = 0;
            char lastOperation = ' ';

            while (index < line.Length)
            {
                char c = line[index++];

                if (char.IsWhiteSpace(c))
                {
                    continue;
                }
                else if (char.IsDigit(c))
                {
                    if (lastOperation != '+' && lastOperation != '*')
                    {
                        result = (int)char.GetNumericValue(c);
                    }
                    else
                    {
                        if (lastOperation == '+')
                        {
                            result += (int)char.GetNumericValue(c);
                            lastOperation = ' ';
                        }
                        else if (lastOperation == '*')
                        {
                            result *= (int)char.GetNumericValue(c);
                            lastOperation = ' ';
                        }
                    }
                }
                else if (c == '(')
                {
                    if (lastOperation != '+' && lastOperation != '*')
                    {
                        result = Evaluate(line, ref index);
                    }
                    else
                    {
                        if (lastOperation == '+')
                        {
                            result += Evaluate(line, ref index);
                            lastOperation = ' ';
                        }
                        else if (lastOperation == '*')
                        {
                            result *= Evaluate(line, ref index);
                            lastOperation = ' ';
                        }
                    }
                }
                else if (c == ')')
                {
                    return result;
                }
                else if (c == '+')
                {
                    lastOperation = '+';
                }
                else if (c == '*')
                {
                    lastOperation = '*';
                }
            }

            return result;
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            long sum = 0;

            foreach (string line in lines)
            {
                int index = 0;
                var temp = Evaluate(Prepare(line), ref index);
                //Console.WriteLine("Sum for " + line + " = " + temp);
                sum += temp;
            }

            Console.WriteLine("Solution: " + sum);
            // Solution: 314455761823725
        }

        private static string Prepare(string line)
        {
            string result = line;
            result = result.Replace("(", "((");
            result = result.Replace(")", "))");
            result = result.Replace(" * ", ") * (");
            result = "(" + result + ")";
            return result;
        }
    }
}
