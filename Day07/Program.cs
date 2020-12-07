using System;
using System.Collections.Generic;
using System.IO;

namespace Day07
{
    class Program
    {
        private static string[] lines;
        private static Dictionary<string, List<Bag>> bags;

        private const string ShinyGoldBag = "shiny gold";

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");
            bags = new Dictionary<string, List<Bag>>();

            foreach (string rule in lines)
            {
                string[] ruleParts = rule.Split("contain");
                string firstBag = ruleParts[0].Split(' ')[0] + " " + ruleParts[0].Split(' ')[1];
                bags.Add(firstBag, new List<Bag>());
                string[] bagList = ruleParts[1].Split(',');
                foreach (string bag in bagList)
                {
                    string trimmedBag = bag.Trim();
                    if (trimmedBag.Contains("no"))
                    {
                        continue;
                    }

                    int amount = Convert.ToInt32(trimmedBag.Split(' ')[0]);
                    string temp = trimmedBag.Split(' ')[1] + " " + trimmedBag.Split(' ')[2];
                    bags[firstBag].Add(new Bag() { Amount = amount, Name = temp });
                }
            }

            SolvePart1();
            SolvePart2();
        }

        struct Bag
        {
            public int Amount;
            public string Name;
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");

            HashSet<string> containedBags = new HashSet<string>();

            foreach (string test in bags.Keys)
            {
                ContainsGoldBag(containedBags, test);
            }

            Console.WriteLine("Solution: " + containedBags.Count);
            // Solution: 274
        }

        private static bool ContainsGoldBag(HashSet<string> containedBags, string testBag)
        {
            foreach (Bag bagFromList in bags[testBag])
            {
                // directly contained or contained by bag from list
                if (bagFromList.Name == ShinyGoldBag || ContainsGoldBag(containedBags, bagFromList.Name))
                {
                    containedBags.Add(testBag);
                    return true;
                }
            }

            return false;
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            int count = CountBags(ShinyGoldBag);

            Console.WriteLine("Solution: " + count);
            // Solution: 158730
        }

        private static int CountBags(string testBag)
        {
            int count = 0;

            foreach (Bag bagFromList in bags[testBag])
            {
                // +1 for bag itself
                count += bagFromList.Amount * (CountBags(bagFromList.Name) + 1);
            }

            return count;
        }
    }
}
