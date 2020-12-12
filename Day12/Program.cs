using System;
using System.IO;

namespace Day12
{
    class Program
    {
        private static string[] lines;

        static void Main(string[] args)
        {
            lines = File.ReadAllLines("input.txt");

            // test case
            //lines = new string[] { "F10", "N3", "F7", "R90", "F11" };

            SolvePart1();
            SolvePart2();
        }

        enum Direction
        {
            East,
            South,
            West,
            North
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            int x = 0, y = 0;
            Direction dir = Direction.East;

            foreach (string line in lines)
            {
                char action = line[0];
                int number = int.Parse(line.Substring(1));
                //Console.WriteLine($"Pos: {x} {y} Dir: {action} {number} {dir}");

                switch (action)
                {
                    case 'N':
                        // north positive
                        y += number;
                        break;
                    case 'S':
                        y -= number;
                        break;
                    case 'E':
                        // east positive
                        x += number;
                        break;
                    case 'W':
                        x -= number;
                        break;
                    case 'L':
                        dir = (Direction)(mod((int)(dir - (number / 90)), 4));
                        break;
                    case 'R':
                        dir = (Direction)(mod((int)(dir + (number / 90)), 4));
                        break;
                    case 'F':
                        switch (dir)
                        {
                            case Direction.East:
                                x += number;
                                break;
                            case Direction.South:
                                y -= number;
                                break;
                            case Direction.West:
                                x -= number;
                                break;
                            case Direction.North:
                                y += number;
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                return;
                        }
                        break;
                }

            }

            Console.WriteLine($"Final: {x} {y}");
            int manhattan = Math.Abs(x) + Math.Abs(y);

            Console.WriteLine("Solution: " + manhattan);
            // Solution: 362
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            int x = 0, y = 0;
            int xrel = 10, yrel = 1;

            foreach (string line in lines)
            {
                char action = line[0];
                int number = int.Parse(line.Substring(1));
                //Console.WriteLine($"Pos: {x} {y} Dir: {action} {number} {dir}");

                switch (action)
                {
                    case 'N':
                        // north positive
                        yrel += number;
                        break;
                    case 'S':
                        yrel -= number;
                        break;
                    case 'E':
                        // east positive
                        xrel += number;
                        break;
                    case 'W':
                        xrel -= number;
                        break;
                    case 'L':
                        for (int i = 0; i < number / 90; i++)
                        {
                            int xtemp = xrel;
                            int ytemp = yrel;
                            xrel = -ytemp;
                            yrel = xtemp;
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < number / 90; i++)
                        {
                            int xtemp = xrel;
                            int ytemp = yrel;
                            xrel = ytemp;
                            yrel = -xtemp;
                        }
                        break;
                    case 'F':
                        x += xrel * number;
                        y += yrel * number;
                        break;
                }

            }

            Console.WriteLine($"Final: {x} {y}");
            int manhattan = Math.Abs(x) + Math.Abs(y);

            Console.WriteLine("Solution: " + manhattan);
            // Solution: 29895
        }
    }
}
