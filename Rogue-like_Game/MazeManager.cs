using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class MazeManager
    {
        //public char[,] maze;
        public int width;
        public int height;
        private Random random = new Random();

        public MazeManager()
        { }
        public MazeManager(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void InitializeMaze(ref char[,] maze)
        {
            maze = new char[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    maze[i, j] = '#'; // Заполняем лабиринт стенами
                }

            }

            // Устанавливаем вход и выход
            maze[0, 1] = 'S'; // Вход
            maze[width - 1, height - 2] = 'E'; // Выход
        }

        public void GenerateMaze(ref char[,] maze, int x, int y)
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

                if (IsInBounds(newX, newY) && maze[newX, newY] == '#')
                {
                    maze[x + direction[0] / 2, y + direction[1] / 2] = ' '; // Открываем проход
                    maze[newX, newY] = ' ';
                    GenerateMaze(ref maze, newX, newY);
                }
            }
        }
        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
    }
}
