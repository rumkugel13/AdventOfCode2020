using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Day17
{
    class Program
    {
        private static string[] lines;
        private static Dictionary<Vector3, char> cubes;
        private static Dictionary<Vector4, char> cubes4;
        private static int CycleCount = 6;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");

            //lines = File.ReadAllLines("sample.txt");
            cubes = new Dictionary<Vector3, char>();
            cubes4 = new Dictionary<Vector4, char>();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    cubes[position] = lines[y][x];

                    Vector4 position4 = new Vector4(position, 0);
                    cubes4[position4] = lines[y][x];
                }
            }

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            Dictionary<Vector3, char> current = new Dictionary<Vector3, char>(cubes);

            for (int i = 0; i < CycleCount; i++)
            {
                Evolve(current, out Dictionary<Vector3, char> outTemp);
                current = outTemp;
            }

            Console.WriteLine("Solution: " + CountCharacters(current));
            // Solution: 257
        }

        private static int CountCharacters(Dictionary<Vector3, char> arrangement)
        {
            int counted = 0;
            foreach (var character in arrangement.Values)
            {
                if (character == '#')
                {
                    counted++;
                }
            }
            return counted;
        }

        private static int CountCharacters(Dictionary<Vector4, char> arrangement)
        {
            int counted = 0;
            foreach (var character in arrangement.Values)
            {
                if (character == '#')
                {
                    counted++;
                }
            }
            return counted;
        }

        private static int CountAdjacent(Dictionary<Vector3, char> arrangement, int x, int y, int z)
        {
            int occupied = 0;
            for (int zadj = z - 1; zadj <= z + 1; zadj++)
            {
                for (int yadj = y - 1; yadj <= y + 1; yadj++)
                {
                    for (int xadj = x - 1; xadj <= x + 1; xadj++)
                    {
                        if (x == xadj && y == yadj && z == zadj)
                            continue;

                        if (arrangement.TryGetValue(new Vector3(xadj, yadj, zadj), out char value))
                        {
                            // for each adjacent
                            if (value == '#')
                            {
                                occupied++;
                            }
                        }
                    }
                }
            }
            return occupied;
        }

        private static int CountAdjacent(Dictionary<Vector4, char> arrangement, int x, int y, int z, int w)
        {
            int occupied = 0;

            for (int wadj = w - 1; wadj <= w + 1; wadj++)
            {
                for (int zadj = z - 1; zadj <= z + 1; zadj++)
                {
                    for (int yadj = y - 1; yadj <= y + 1; yadj++)
                    {
                        for (int xadj = x - 1; xadj <= x + 1; xadj++)
                        {
                            if (x == xadj && y == yadj && z == zadj && w == wadj)
                                continue;

                            if (arrangement.TryGetValue(new Vector4(xadj, yadj, zadj, wadj), out char value))
                            {
                                // for each adjacent
                                if (value == '#')
                                {
                                    occupied++;
                                }
                            }
                        }
                    }
                }
            }
            return occupied;
        }

        private static void Evolve(Dictionary<Vector3, char> arrangement, out Dictionary<Vector3, char> result)
        {
            result = new Dictionary<Vector3, char>();
            Vector3 min = new Vector3(float.MaxValue), max = new Vector3(float.MinValue);

            foreach (var position in arrangement.Keys)
            {
                min = Vector3.Min(min, position);
                max = Vector3.Max(max, position);
            }

            // check edges too
            for (int z = (int)min.Z - 1; z <= max.Z + 1; z++)
            {
                for (int y = (int)min.Y - 1; y <= max.Y + 1; y++)
                {
                    for (int x = (int)min.X - 1; x <= max.X + 1; x++)
                    {
                        Vector3 position = new Vector3(x, y, z);

                        int occupied = CountAdjacent(arrangement, (int)position.X, (int)position.Y, (int)position.Z);

                        if (!arrangement.ContainsKey(position) || arrangement[position] == '.')
                        {
                            if (occupied == 3)
                            {
                                result[position] = '#';
                            }
                            else
                            {
                                result[position] = '.';
                            }
                        }
                        else
                        {
                            if (occupied == 2 || occupied == 3)
                            {
                                result[position] = '#';
                            }
                            else
                            {
                                result[position] = '.';
                            }
                        }
                    }
                }
            }
        }

        private static void Evolve(Dictionary<Vector4, char> arrangement, out Dictionary<Vector4, char> result)
        {
            result = new Dictionary<Vector4, char>();
            Vector4 min = new Vector4(float.MaxValue), max = new Vector4(float.MinValue);

            foreach (var position in arrangement.Keys)
            {
                min = Vector4.Min(min, position);
                max = Vector4.Max(max, position);
            }

            // check edges too
            for (int w = (int)min.W - 1; w <= max.W + 1; w++)
            {
                for (int z = (int)min.Z - 1; z <= max.Z + 1; z++)
                {
                    for (int y = (int)min.Y - 1; y <= max.Y + 1; y++)
                    {
                        for (int x = (int)min.X - 1; x <= max.X + 1; x++)
                        {
                            Vector4 position = new Vector4(x, y, z, w);

                            int occupied = CountAdjacent(arrangement, (int)position.X, (int)position.Y, (int)position.Z, (int)position.W);

                            if (!arrangement.ContainsKey(position) || arrangement[position] == '.')
                            {
                                if (occupied == 3)
                                {
                                    result[position] = '#';
                                }
                                else
                                {
                                    result[position] = '.';
                                }
                            }
                            else
                            {
                                if (occupied == 2 || occupied == 3)
                                {
                                    result[position] = '#';
                                }
                                else
                                {
                                    result[position] = '.';
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            Dictionary<Vector4, char> current = new Dictionary<Vector4, char>(cubes4);

            for (int i = 0; i < CycleCount; i++)
            {
                Evolve(current, out Dictionary<Vector4, char> outTemp);
                current = outTemp;
            }

            Console.WriteLine("Solution: " + CountCharacters(current));
            // Solution: 2532
        }
    }
}
