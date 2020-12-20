using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day20
{
    class Program
    {
        private static Dictionary<int, Tile> tiles;

        static void Main(string[] args)
        {
            tiles = new Dictionary<int, Tile>();
            var lines = File.ReadAllLines("input.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                int id = int.Parse(lines[i++].Split(' ')[1].TrimEnd(':'));
                var image = new string[10];

                for (int j = 0; j < 10; j++)
                {
                    image[j] = lines[i++];
                }

                Tile tile;
                tile.Neighbours = new int[4];
                tile.Orientations = new string[4][];
                tile.Orientations[0] = image;
                tile.Orientations[1] = RotateCW(tile.Orientations[0]);
                tile.Orientations[2] = RotateCW(tile.Orientations[1]);
                tile.Orientations[3] = RotateCW(tile.Orientations[2]);

                //PrintTile(tile);
                tiles[id] = tile;
            }

            SolvePart1();
            SolvePart2();
        }

        private static void PrintTile(Tile tile)
        {
            for (int i = 0; i < tile.Orientations.Length; i++)
            {
                foreach (var item in tile.Orientations[i])
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
            }
        }

        //private static string[] Flip(string[] image)
        //{

        //}

        private static string[] RotateCW(string[] image)
        {
            var rotated = new string[image.Length];

            for (int x = 0; x < image.Length; x++)
            {
                StringBuilder builder = new StringBuilder();
                for (int y = 0; y < image.Length; y++)
                {
                    builder.Append(image[y][x]);
                }
                rotated[x] = new string(builder.ToString().Reverse().ToArray());
            }

            return rotated;
        }

        enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        struct Tile
        {
            public int[] Neighbours;
            public string[][] Orientations;
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");



            //Console.WriteLine("Solution: " + count);
            // Solution: -
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            //Console.WriteLine("Solution: " + count);
            // Solution: -
        }
    }
}
