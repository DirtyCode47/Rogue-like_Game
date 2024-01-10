using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRogueLike
{
    internal class Maze
    {
        private int _width;
        private int _height;

        public Maze(int width, int height)
        {
            _width = width;
            _height = height;
        }
        public char[,] Map;
        public int Width { get; set; } = 23;
        public int Height { get; set; } = 23;
    }
}
