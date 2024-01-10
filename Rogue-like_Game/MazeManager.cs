using Rogue_like_Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRogueLike
{
    internal static class MazeManager
    {
        public static void CreateMaze(Maze maze, Zombie zombie, Archer archer)
        {
            InitializeMaze(maze);
            GenerateMaze(maze,1,1);
            LocateEnemiesSymbols(maze, zombie, archer);
        }
        private static void InitializeMaze(Maze maze)
        {
            maze.Map = new char[maze.Width, maze.Height];

            for (int i = 0; i < maze.Width; i++)
            {
                for (int j = 0; j < maze.Height; j++)
                {
                    maze.Map[i, j] = '#'; // Заполняем лабиринт стенами
                }

            }

            maze.Map[1, 0] = 'S'; // Вход
            maze.Map[maze.Width - 2, maze.Height - 1] = 'E'; // Выход
            maze.Map[1, 1] = 'P'; //игрок
        }

        private static void GenerateMaze(Maze maze, int x, int y)
        {
            List<int[]> directions = new List<int[]>
        {
            new int[] { 0, 2 },
            new int[] { 2, 0 },
            new int[] { 0, -2 },
            new int[] { -2, 0 }
        };

            Shuffle(directions);

            foreach (var direction in directions)
            {
                int newX = x + direction[0];
                int newY = y + direction[1];

                if (IsInBounds(maze, newX, newY) && maze.Map[newX, newY] == '#')
                {
                    maze.Map[x + direction[0] / 2, y + direction[1] / 2] = ' '; // Открываем проход
                    maze.Map[newX, newY] = ' ';
                    GenerateMaze(maze, newX, newY);
                }
            }
        }
        public static void LocateEnemiesSymbols(Maze maze, Zombie zombie, Archer archer) //проставляем символы врагов на карте
        {
            maze.Map[zombie.X, zombie.Y] = 'Z';
            maze.Map[archer.X, archer.Y] = 'A';
        }
        private static void Shuffle<T>(List<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private static bool IsInBounds(Maze maze, int x, int y)
        {
            return x >= 0 && x < maze.Width && y >= 0 && y < maze.Height;
        }
    }
}
