using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.MazeLogic
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
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
    }
}
