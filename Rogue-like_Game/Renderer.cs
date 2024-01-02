using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal static class Renderer
    {

        public static void PrintMaze(Maze maze)
        {
            Console.Clear();

            for(int i=0;i<maze.height;i++)
            {
                for(int j=0;j<maze.width;j++)
                {
                    Console.Write(maze.map[i,j]+" "); //Рисуем доп пробел, чтобы решить проблему, что лабиринт смотрится не очень симметрично
                }

                Console.WriteLine();
            }
        }
    }
}
