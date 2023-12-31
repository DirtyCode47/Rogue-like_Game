using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Maze
    {
        public char[,] map;
        public int width = 23; //было 23
        public int height = 23; 

        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
