using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day20
{
    class Program
    {
        private static List<Tile> tiles;

        static void Main(string[] args)
        {
            tiles = new List<Tile>();
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
                tile.ID = id;
                tile.Neighbours = new List<int>();
                tile.Orientations = new string[8][];

                tile.Orientations[0] = image;
                tile.Orientations[1] = RotateCW(tile.Orientations[0]);
                tile.Orientations[2] = RotateCW(tile.Orientations[1]);
                tile.Orientations[3] = RotateCW(tile.Orientations[2]);
                tile.Orientations[4] = Flip(tile.Orientations[0]);
                tile.Orientations[5] = Flip(tile.Orientations[1]);
                tile.Orientations[6] = Flip(tile.Orientations[2]);
                tile.Orientations[7] = Flip(tile.Orientations[3]);

                //PrintTile(tile);
                tiles.Add(tile);
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

        private static string[] Flip(string[] image)
        {
            string[] flipped = new string[image.Length];

            for (int i = 0; i < image.Length; i++)
            {
                var temp = image[i].ToCharArray();
                Array.Reverse(temp);
                flipped[i] = new string(temp);
            }

            return flipped;
        }

        private static string[] RotateCW(string[] image)
        {
            var rotated = new string[image.Length];

            for (int x = 0; x < image.Length; x++)
            {
                StringBuilder builder = new StringBuilder();
                for (int y = image.Length - 1; y >= 0; y--)
                {
                    builder.Append(image[y][x]);
                }
                rotated[x] = builder.ToString();
            }

            return rotated;
        }

        struct Tile
        {
            public int ID;
            public List<int> Neighbours;
            public string[][] Orientations;
        }

        struct Neighbour
        {
            public int ID;
            public int Edge;
        }

        private static void SolvePart1()
        {
            Console.WriteLine("Solving Part 1");
            long result = 1;

            for (int i = 0; i < tiles.Count; i++)
            {
                Tile A = tiles[i];
                for (int j = i + 1; j < tiles.Count; j++)
                {
                    Tile B = tiles[j];
                    if (IsNeighbour(A, B))
                    {
                        tiles[i].Neighbours.Add(j);
                        tiles[j].Neighbours.Add(i);
                    }
                }
                if (tiles[i].Neighbours.Count == 2)
                {
                    result *= A.ID;
                    Console.WriteLine(A.ID);
                }
            }

            Console.WriteLine("Solution: " + result);
            // Solution: 140656720229539
        }

        private static bool IsNeighbour(Tile A, Tile B)
        {
            for (int i = 0; i < A.Orientations.Length; i++)
            {
                for (int j = 0; j < B.Orientations.Length; j++)
                {
                    if (A.Orientations[i][0] == B.Orientations[j][0])
                    { 
                        return true; 
                    }
                }
            }

            return false;
        }

        private static void SolvePart2()
        {
            Console.WriteLine("Solving Part 2");

            //Console.WriteLine("Solution: " + count);
            // Solution: -
        }
    }
}
