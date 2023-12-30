using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Renderer
    {
        public Renderer() { }

        public void PrintMaze(char[,] maze, int width, int height)
        {
            Console.Clear();
            for(int i=0;i<height;i++)
            {
                for(int j=0;j<width;j++)
                {
                    Console.Write(maze[i,j]+" ");
                }
                Console.WriteLine();
            }
        }
    }
}
