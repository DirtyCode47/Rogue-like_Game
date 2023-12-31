using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal static class MazeManager
    {
        //public char[,] maze;
        //public int width;
        //public int height;
      

        //public MazeManager()
        //{ }
        //public MazeManager(int width, int height)
        //{
        //    this.width = width;
        //    this.height = height;
        //}

        public static void InitializeMaze(Maze maze)
        {
            maze.map = new char[maze.width, maze.height];

            for (int i = 0; i < maze.width; i++)
            {
                for (int j = 0; j < maze.height; j++)
                {
                    maze.map[i, j] = '#'; // Заполняем лабиринт стенами
                }

            }

            // Устанавливаем вход и выход
            maze.map[1, 0] = 'S'; // Вход
            maze.map[maze.width - 2, maze.height - 1] = 'E'; // Выход
            maze.map[1, 1] = 'P';
        }

        public static void GenerateMaze(Maze maze, int x, int y)
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

                if (IsInBounds(maze, newX, newY) && maze.map[newX, newY] == '#')
                {
                    maze.map[x + direction[0] / 2, y + direction[1] / 2] = ' '; // Открываем проход
                    maze.map[newX, newY] = ' ';
                    GenerateMaze(maze, newX, newY);
                }
            }
        }
        public static void SpawnEnemies(Maze maze,Zombie zombie)
        {
            maze.map[zombie.X, zombie.Y] = 'Z';
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
            return x >= 0 && x < maze.width && y >= 0 && y < maze.height;
        }
    }
}
