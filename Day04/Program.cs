using System;
using System.Collections.Generic;
using System.IO;

namespace Day04
{
    class Program
    {
        private static List<string> items = new List<string>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            // valid lines
            //lines = new string[]
            //{
            //    "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
            //    "hcl:#623a2f",
            //    "",
            //    "eyr:2029 ecl:blu cid:129 byr:1989",
            //    "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
            //    "",
            //    "hcl:#888785",
            //    "hgt:164cm byr:2001 iyr:2015 cid:88",
            //    "pid:545766238 ecl:hzl",
            //    "eyr:2022",
            //    "",
            //    "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
            //};

            // invalid lines
            //lines = new string[]
            //{
            //    "eyr:1972 cid:100",
            //    "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
            //    "",
            //    "iyr:2019",
            //    "hcl:#602927 eyr:1967 hgt:170cm",
            //    "ecl:grn pid:012533040 byr:1946",
            //    "",
            //    "hcl:dab227 iyr:2012",
            //    "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
            //    "",
            //    "hgt:59cm ecl:zzz",
            //    "eyr:2038 hcl:74454a iyr:2023",
            //    "pid:3556412378 byr:2007",
            //};

            string buffer = "";

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    items.Add(buffer.Trim());
                    buffer = "";
                    continue;
                }

                buffer += lines[i] + " ";
            }

            // don't forget last one
            items.Add(buffer.Trim());

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            int valid = 0;

            foreach (string item in items)
            {
                int count = 0;

                for (int i = 0; i < keywords.Length; i++)
                {
                    if (item.Contains(keywords[i] + ":") )
                    {
                        count++;
                    }
                }

                if (count == 8 || (count == 7 && !item.Contains("cid:")))
                {
                    valid++;
                }
            }

            Console.WriteLine("Solution: " + valid);
            // Solution: 213
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            int valid = 0;

            foreach (string item in items)
            {
                int presentCount = 0;

                for (int i = 0; i < keywords.Length; i++)
                {
                    if (item.Contains(keywords[i] + ":"))
                    {
                        presentCount++;
                    }
                }

                if (presentCount == 8 || (presentCount == 7 && !item.Contains("cid:")))
                {
                    // check validity of fields
                    int validCount = 0;

                    string[] valueFields = item.Split(' ');

                    foreach (string valueField in valueFields)
                    {
                        string[] fields = valueField.Split(':');

                        switch (fields[0])
                        {
                            case "byr":
                                if (IsValidNumberBetween(fields[1], 4, 1920, 2002))
                                {
                                    validCount++;
                                }
                                break;
                            case "iyr":
                                if (IsValidNumberBetween(fields[1], 4, 2010, 2020))
                                {
                                    validCount++;
                                }
                                break;
                            case "eyr":
                                if (IsValidNumberBetween(fields[1], 4, 2020, 2030))
                                {
                                    validCount++;
                                }
                                break;
                            case "hgt":
                                if (fields[1].Contains("cm"))
                                {
                                    string number = fields[1].Substring(0, 3);
                                    if (IsValidNumberBetween(number, 3, 150, 193))
                                    {
                                        validCount++;
                                    }
                                }
                                else if (fields[1].Contains("in"))
                                {
                                    string number = fields[1].Substring(0, 2);
                                    if (IsValidNumberBetween(number, 2, 59, 76))
                                    {
                                        validCount++;
                                    }
                                }
                                break;
                            case "hcl":
                                if (fields[1].StartsWith("#"))
                                {
                                    bool invalid = false;
                                    const string check = "0123456789abcdef";
                                    for (int i = 1; i < fields[1].Length; i++)
                                    {
                                        if (!check.Contains(fields[1][i]))
                                        {
                                            invalid = true;
                                            break;
                                        }
                                    }

                                    if (!invalid)
                                    {
                                        validCount++;
                                    }
                                }
                                break;
                            case "ecl":
                                switch (fields[1])
                                {
                                    case "amb":
                                    case "blu":
                                    case "brn":
                                    case "gry":
                                    case "grn":
                                    case "hzl":
                                    case "oth":
                                        validCount++;
                                        break;
                                }

                                break;
                            case "pid":
                                if (IsValidNumberBetween(fields[1], 9, 000000000, 999999999))
                                {
                                    validCount++;
                                }
                                break;
                            case "cid":
                                break;
                        }
                    }

                    if (validCount >= 7)
                    {
                        valid++;
                    }
                }
            }

            Console.WriteLine("Solution: " + valid);
            // Solution: 147
        }

        private static bool IsValidNumberBetween(string data, int charCount, int min, int max)
        {
            if (data.Length == charCount && int.TryParse(data, out int value))
            {
                if (value >= min && value <= max)
                {
                    return true;
                }
            }

            return false;
        }

        private readonly static string[] keywords = {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid",
            "cid"
        };
    }
}
