using System;
using System.IO;

namespace Day11
{
    class Program
    {
        private static string[] lines;
        private static char[][] seats;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");

            //lines = File.ReadAllLines("test.txt");
            seats = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                seats[i] = lines[i].ToCharArray();
            }

            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            char[][] current = seats;
            //PrintArrangement(outTemp);
            bool changed;
            do
            {
                changed = Evolve(current, out char[][] outTemp, false);
                current = outTemp;
                //PrintArrangement(outTemp);
            }
            while (changed);

            Console.WriteLine("Solution: " + CountCharacters(current));
            // Solution: 2494
        }

        private static int CountCharacters(char[][] arrangement)
        {
            int counted = 0;
            foreach (char[] temp in arrangement)
            {
                foreach (char character in temp)
                {
                    if (character == '#')
                    {
                        counted++;
                    }
                }
            }
            return counted;
        }

        private static void PrintArrangement(char[][] arrangement)
        {
            foreach (char[] temp in arrangement)
            {
                Console.WriteLine(temp);
            }
            Console.WriteLine();
        }

        private static int CountAdjacent(char[][] arrangement, int x, int y)
        {
            int occupied = 0;
            for (int yadj = y - 1; yadj <= y + 1; yadj++)
            {
                if (yadj >= 0 && yadj < arrangement.Length)
                {
                    for (int xadj = x - 1; xadj <= x + 1; xadj++)
                    {
                        if (xadj >= 0 && xadj < arrangement[y].Length)
                        {
                            if (x == xadj && y == yadj)
                                continue;

                            // for each adjacent
                            if (arrangement[yadj][xadj] == '#')
                            {
                                occupied++;
                            }
                        }
                    }
                }
            }
            return occupied;
        }

        private static int CountAdjacentDirections(char[][] arrangement, int x, int y)
        {
            int occupied = 0;
            for (int ydir = -1; ydir <= +1; ydir++)
            {
                for (int xdir = -1; xdir <= +1; xdir++)
                {
                    if (xdir == 0 && ydir == 0)
                        continue;

                    int xcurrent = x;
                    int ycurrent = y;

                    do
                    {
                        xcurrent += xdir;
                        ycurrent += ydir;

                        if (ycurrent >= 0 && ycurrent < arrangement.Length &&
                            xcurrent >= 0 && xcurrent < arrangement[y].Length)
                        {
                            // if place exists
                            if (arrangement[ycurrent][xcurrent] == '#')
                            {
                                occupied++;
                                break;
                            }
                            else if (arrangement[ycurrent][xcurrent] == 'L')
                            {
                                break;
                            }
                        }
                        // continue in this direction
                    }
                    while (ycurrent >= 0 && ycurrent < arrangement.Length &&
                            xcurrent >= 0 && xcurrent < arrangement[y].Length);
                }
            }
            return occupied;
        }

        private static bool Evolve(char[][] arrangement, out char[][] result, bool direction)
        {
            bool changed = false;
            result = new char[arrangement.Length][];

            for (int y = 0; y < arrangement.Length; y++)
            {
                result[y] = new char[arrangement[y].Length];
                for (int x = 0; x < arrangement[y].Length; x++)
                {
                    if (arrangement[y][x] == '.')
                    {
                        // no seat here
                        result[y][x] = arrangement[y][x];
                        continue;
                    }

                    int occupied = direction ? CountAdjacentDirections(arrangement, x, y) : CountAdjacent(arrangement, x, y);

                    if (occupied == 0)
                    {
                        // occupy seat
                        result[y][x] = '#';
                    }
                    else if (occupied >= (direction ? 5 : 4))
                    {
                        // empty seat
                        result[y][x] = 'L';
                    }
                    else
                    {
                        result[y][x] = arrangement[y][x];
                    }

                    if (result[y][x] != arrangement[y][x])
                    {
                        changed = true;
                    }
                }
            }

            return changed;
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");
            char[][] current = seats;
            //PrintArrangement(outTemp);
            bool changed;

            do
            {
                changed = Evolve(current, out char[][] outTemp, true);
                current = outTemp;
                //PrintArrangement(outTemp);
            }
            while (changed);

            Console.WriteLine("Solution: " + CountCharacters(current));
            // Solution: 2306
        }
    }
}
