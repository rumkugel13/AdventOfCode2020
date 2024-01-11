using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

            var placed = PlaceImages();

            foreach (var row in placed)
            {
                foreach (var col in row)
                {
                    Console.Write(tiles[col].ID + " ");
                }
                Console.WriteLine();
            }

            var oriented = OrientImages(placed);

            foreach (var row in oriented)
            {
                foreach (var col in row)
                {
                    Console.Write("" + col + " ");
                }
                Console.WriteLine();
            }

            var image = MakeImage(placed, oriented);

            var pattern = File.ReadAllLines("pattern.txt");

            var image2 = RotateCW(image);
            var image3 = RotateCW(image2);
            var image4 = RotateCW(image3);
            var flipped = Flip(image);
            var flipped2 = Flip(image2);
            var flipped3 = Flip(image3);
            var flipped4 = Flip(image4);

            string[][] orientations = new string[][]
            {
                image, image2, image3, image4, flipped, flipped2, flipped3, flipped4
            };

            var result = 0;
            foreach (var orient in orientations)
            {
                var points = FindPatternInImage(pattern, orient);
                if (points.Count > 0)
                {
                    result = Count(orient, '#') - (points.Count * Count(pattern, '#'));
                    break;
                }
            }

            Console.WriteLine("Solution: " + result);
            // Solution: 1885
        }

        private static int Count(string[] image, char c)
        {
            int count = 0;
            foreach (var row in image)
            {
                foreach (char ch in row)
                {
                    if (ch == c)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static List<Point> FindPatternInImage(string[] pattern, string[] image)
        {
            List<Point> points = new List<Point>();
            for (int row = 0; row < image.Length - pattern.Length; row++)
            {
                for (int col = 0; col < image[row].Length - pattern[0].Length; col++)
                {
                    if (SubstringMatch(image[row + 0].Substring(col, pattern[0].Length), pattern[0]) &&
                        SubstringMatch(image[row + 1].Substring(col, pattern[0].Length), pattern[1]) &&
                        SubstringMatch(image[row + 2].Substring(col, pattern[0].Length), pattern[2]))
                    {
                        points.Add(new Point(col, row));
                    }
                }
            }

            return points;
        }

        private static bool SubstringMatch(string image, string pattern)
        {
            for (int c = 0; c < pattern.Length; c++)
            {
                if (pattern[c] != '#')
                    continue;
                if (image[c] != '#')
                {
                    return false;
                }
            }
            return true;
        }

        private static string[] MakeImage(int[][] placement, int[][] orientation)
        {
            string[] image = new string[(placement.Length) * (tiles[0].Orientations[0].Length - 2)];

            for (int row = 0; row < placement.Length; row++)
            {
                string[] newRows = new string[(tiles[0].Orientations[0].Length - 2)];
                for (int col = 0; col < placement[row].Length; col++)
                {
                    var finalPart = tiles[placement[row][col]].Orientations[orientation[row][col]];
                    for (int partRow = 1; partRow < finalPart.Length - 1; partRow++)
                    {
                        newRows[partRow - 1] += finalPart[partRow].Substring(1, finalPart[partRow].Length - 2);
                    }
                }
                for (int newRow = 0; newRow < newRows.Length; newRow++)
                {
                    image[(row * newRows.Length) + newRow] = newRows[newRow];
                }
            }

            return image;
        }

        private static int[][] OrientImages(int[][] placed)
        {
            int[][] orientations = new int[placed.Length][];
            orientations[0] = new int[placed[0].Length];

            int cornerIndex = placed[0][0];
            int right = placed[0][1];
            int down = placed[1][0];

            for (int i = 0; i < tiles[cornerIndex].Orientations.Length; i++)
            {
                for (int j = 0; j < tiles[down].Orientations.Length; j++)
                {
                    if (tiles[cornerIndex].Orientations[i][^1] == tiles[down].Orientations[j][0])
                    {
                        //check right
                        for (int k = 0; k < tiles[right].Orientations.Length; k++)
                        {
                            bool found = true;
                            for (int col = 0; col < tiles[right].Orientations[k].Length; col++)
                            {
                                if (tiles[cornerIndex].Orientations[i][col][tiles[cornerIndex].Orientations[0].Length - 1] !=
                                    tiles[right].Orientations[k][col][0])
                                {
                                    found = false;
                                    break;
                                }
                            }
                            if (found)
                            {
                                orientations[0][0] = i;
                                orientations[0][1] = k;
                                break;
                            }
                        }
                    }
                }
            }

            for (int col = 2; col < placed[0].Length; col++)
            {
                int prev = placed[0][col - 1];
                Tile prevTile = tiles[prev];
                Tile secondTile = tiles[placed[0][col]];
                for (int j = 0; j < secondTile.Orientations.Length; j++)
                {
                    bool found = true;
                    for (int k = 0; k < secondTile.Orientations[j].Length; k++)
                    {
                        if (prevTile.Orientations[orientations[0][col - 1]][k][prevTile.Orientations[0].Length - 1] !=
                            secondTile.Orientations[j][k][0])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        orientations[0][col] = j; 
                        break;
                    }
                }
            }

            for (int row = 1; row < placed.Length; row++)
            {
                orientations[row] = new int[placed[row].Length];
                for (int col = 0; col < placed[row].Length; col++)
                {
                    Tile prevTile = tiles[placed[row - 1][col]];
                    Tile secondTile = tiles[placed[row][col]];
                    for (int j = 0; j < secondTile.Orientations.Length; j++)
                    {
                        if (prevTile.Orientations[orientations[row - 1][col]][prevTile.Orientations[0].Length - 1] == secondTile.Orientations[j][0])
                        {
                            orientations[row][col] = j;
                            break;
                        }
                    }
                }
            }

            return orientations;
        }

        private static int[][] PlaceImages()
        {
            int[][] grid = new int[(int)Math.Sqrt(tiles.Count)][];
            int cornerIndex = 0;
            for (int i = 0; i < tiles.Count; i++)
            {
                Tile tile = tiles[i];
                if (tile.Neighbours.Count == 2)
                {
                    cornerIndex = i;
                    break;
                }
            }

            HashSet<int> placed = new HashSet<int>();
            grid[0] = new int[(int)Math.Sqrt(tiles.Count)];
            grid[0][0] = cornerIndex;
            grid[0][1] = tiles[cornerIndex].Neighbours[0];
            placed.Add(cornerIndex);
            placed.Add(tiles[cornerIndex].Neighbours[0]);

            for (int i = 2; i < grid[0].Length; i++)
            {
                int prev = grid[0][i - 1];

                foreach (int neighbour in tiles[prev].Neighbours)
                {
                    if (tiles[neighbour].Neighbours.Count < 4 && !placed.Contains(neighbour))
                    {
                        grid[0][i] = neighbour;
                        placed.Add(neighbour);
                        break;
                    }
                }
            }

            for (int row = 1; row < grid.Length; row++)
            {
                grid[row] = new int[(int)Math.Sqrt(tiles.Count)];
                for (int col = 0; col < grid[row].Length; col++)
                {
                    int prev = grid[row - 1][col];
                    foreach (int neighbour in tiles[prev].Neighbours)
                    {
                        if (!placed.Contains(neighbour))
                        {
                            grid[row][col] = neighbour;
                            placed.Add(neighbour);
                            break;
                        }
                    }
                }
            }

            return grid;
        }
    }
}
