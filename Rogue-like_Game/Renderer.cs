using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Renderer
    {
        public static void PrintMaze(Maze maze)
        {
            Console.Clear();

            for (int i = 0; i < maze.Height; i++)
            {
                for (int j = 0; j < maze.Width; j++)
                {
                    Console.Write(maze.Map[i, j] + " "); //Рисуем доп пробел, чтобы решить проблему, что лабиринт смотрится не очень симметрично
                }

                Console.WriteLine();
            }
        }
    }
}
