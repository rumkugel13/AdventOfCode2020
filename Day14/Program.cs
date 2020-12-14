using System;
using System.Collections.Generic;
using System.IO;

namespace Day14
{
    class Program
    {
        private static string[] lines;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");
            //lines = File.ReadAllLines("sample.txt");
            //lines = File.ReadAllLines("sample2.txt");

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            Dictionary<int, long> values = new Dictionary<int, long>();
            string mask = "";

            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split('=')[1].Trim();
                    continue;
                }

                {
                    int memAdress = int.Parse(line.Split('=')[0].Split('[')[1].Split(']')[0]);
                    long value = long.Parse(line.Split('=')[1].Trim());

                    string valueAsBin = Convert.ToString(value, 2).PadLeft(mask.Length, '0');

                    char[] modifiedValue = new char[valueAsBin.Length];

                    for (int i = valueAsBin.Length - 1; i >= 0; i--)
                    {
                        modifiedValue[i] = mask[i] == 'X' ? valueAsBin[i] : mask[i];
                    }

                    values[memAdress] = Convert.ToInt64(new string(modifiedValue), 2);
                }
            }

            long sum = 0;
            foreach (var value in values.Values)
            {
                sum += value;
            }
            Console.WriteLine("Solution: " + sum);
            // Solution: 17481577045893
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            Dictionary<long, long> values = new Dictionary<long, long>();
            string mask = "";

            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split('=')[1].Trim();
                    continue;
                }

                {
                    int memAddress = int.Parse(line.Split('=')[0].Split('[')[1].Split(']')[0]);
                    long value = long.Parse(line.Split('=')[1].Trim());

                    string addressAsBin = Convert.ToString(memAddress, 2).PadLeft(mask.Length, '0');

                    char[] modifiedAddress = new char[addressAsBin.Length];

                    for (int i = addressAsBin.Length - 1; i >= 0; i--)
                    {
                        modifiedAddress[i] = mask[i] == '0' ? addressAsBin[i] : (mask[i] == '1' ? '1' : 'X');
                    }

                    WriteToAddress(values, modifiedAddress, value);
                }
            }

            long sum = 0;
            foreach (var value in values.Values)
            {
                sum += value;
            }
            Console.WriteLine("Solution: " + sum);
            // Solution: 4160009892257
        }

        private static void WriteToAddress(Dictionary<long, long> values, char[] address, long value)
        {
            string addressString = new string(address);
            if (addressString.Contains('X'))
            {
                int index = addressString.IndexOf('X');
                char[] newAddress = (char[])address.Clone();
                newAddress[index] = '0';
                WriteToAddress(values, newAddress, value);

                newAddress[index] = '1';
                WriteToAddress(values, newAddress, value);
            }
            else
            {
                long addressValue = Convert.ToInt64(addressString, 2);
                values[addressValue] = value;
            }
        }
    }
}
