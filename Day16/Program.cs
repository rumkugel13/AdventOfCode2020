using System;
using System.Collections.Generic;
using System.IO;

namespace Day16
{
    class Program
    {
        private static List<Rule> rules;
        private static int[] yourTicket;
        private static List<int[]> nearbyTickets;
        private static List<int> invalidTickets;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            ParseInput(lines);
            SolvePart1();
            SolvePart2();
        }

        struct Rule
        {
            public string Name;
            public int[] Ranges;
            public int Column;
        }

        private static void ParseInput(string[] lines)
        {
            int currentLine = 0;

            rules = new List<Rule>();
            do
            {
                Rule rule = new Rule();
                var data = lines[currentLine].Split(':');
                rule.Name = data[0].Trim();
                var ranges = data[1].Trim().Replace(" or ", "-").Split('-');
                rule.Ranges = new int[ranges.Length];
                for (int i = 0; i < ranges.Length; i++)
                {
                    rule.Ranges[i] = int.Parse(ranges[i]);
                }
                rules.Add(rule);
                currentLine++;
            }
            while (!string.IsNullOrEmpty(lines[currentLine]));
            currentLine += 2;  // your ticket: and then numbers

            yourTicket = new int[rules.Count];

            {
                var numbers = lines[currentLine].Split(',');
                for (int i = 0; i < numbers.Length; i++)
                {
                    yourTicket[i] = int.Parse(numbers[i]);
                }
            }

            currentLine += 3;  // empty line, nearby ticket: and then numbers

            nearbyTickets = new List<int[]>();
            while (!(currentLine == lines.Length))
            {
                var ticket = new int[rules.Count];
                var numbers = lines[currentLine].Split(',');
                for (int i = 0; i < numbers.Length; i++)
                {
                    ticket[i] = int.Parse(numbers[i]);
                }
                nearbyTickets.Add(ticket);
                currentLine++;
            }
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            int errorRate = 0;

            invalidTickets = new List<int>();
            for (int i = 0; i < nearbyTickets.Count; i++)
            {
                int[] ticket = nearbyTickets[i];
                bool validTicket = true;

                foreach (var number in ticket)
                {
                    bool validNumber = false;
                    foreach (Rule rule in rules)
                    {
                        if ((number >= rule.Ranges[0] && number <= rule.Ranges[1]) ||
                            (number >= rule.Ranges[2] && number <= rule.Ranges[3]))
                        {
                            validNumber = true;
                        }
                    }

                    if (!validNumber)
                    {
                        errorRate += number;
                        validTicket = false;
                    }
                }

                if (!validTicket)
                {
                    invalidTickets.Add(i);
                }
            }

            Console.WriteLine("Solution: " + errorRate);
            // Solution: 25961
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            List<int> rulesToProcess = new List<int>(System.Linq.Enumerable.Range(0, rules.Count));
            List<int> columnProcessed = new List<int>();

            while (rulesToProcess.Count > 0)
            {
                for (int r = 0; r < rulesToProcess.Count; r++)
                {
                    int ruleToProcess = rulesToProcess[r];
                    Rule rule = rules[ruleToProcess];
                    List<int> validColumns = new List<int>(System.Linq.Enumerable.Range(0, rules.Count));

                    foreach (int c in columnProcessed)
                    {
                        validColumns.Remove(c);
                    }

                    for (int column = 0; column < rules.Count; column++)
                    {
                        for (int i = 0; i < nearbyTickets.Count; i++)
                        {
                            if (invalidTickets.Contains(i)) continue;

                            int[] ticket = nearbyTickets[i];
                            int number = ticket[column];

                            if ((number < rule.Ranges[0] || number > rule.Ranges[1]) &&
                                (number < rule.Ranges[2] || number > rule.Ranges[3]))
                            {
                                validColumns.Remove(column);
                            }
                        }
                    }

                    if (validColumns.Count == 1)
                    {
                        rules[ruleToProcess] = new Rule() { Name = rule.Name, Ranges = rule.Ranges, Column = validColumns[0] };
                        rulesToProcess.Remove(ruleToProcess);
                        columnProcessed.Add(validColumns[0]);
                    }
                }
            }

            long result = 1;
            foreach (Rule r in rules)
            {
                if (r.Name.StartsWith("departure"))
                {
                    result *= yourTicket[r.Column];
                }
            }

            Console.WriteLine("Solution: " + result);
            // Solution: 603409823791
        }
    }
}
