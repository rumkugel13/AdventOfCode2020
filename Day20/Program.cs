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
                tile.Edges = new string[8];

                tile.Edges[0] = new string(image[0].ToCharArray());
                tile.Edges[2] = new string(image[image.Length - 1].ToCharArray());
                var temp = RotateCW(image);
                tile.Edges[1] = new string(temp[temp.Length - 1].ToCharArray());
                tile.Edges[3] = new string(temp[0].ToCharArray());
                for (int j = 0; j < 4; j++)
                {
                    var newArray = tile.Edges[j].ToCharArray();
                    Array.Reverse(newArray);
                    tile.Edges[j + 4] = new string(newArray);
                }

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
            public int ID;
            public List<int> Neighbours;
            public string[][] Orientations;
            public string[] Edges;
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
            foreach (var edgeA in A.Edges)
            {
                foreach (var edgeB in B.Edges)
                {
                    if (edgeA == edgeB)
                        return true;
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
