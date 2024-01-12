using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.MazeLogic
{
    internal class Maze
    {
        private int width;
        private int height;

        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public char[,] Map;
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
    }
}
