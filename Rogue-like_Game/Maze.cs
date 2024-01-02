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
        public int width { get; set; } = 23; 
        public int height { get; set; } = 23; 

        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
