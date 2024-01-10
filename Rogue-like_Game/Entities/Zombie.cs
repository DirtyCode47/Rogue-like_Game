using MazeRogueLike;
using MazeRogueLike.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Zombie:Enemy
    {
        public Zombie(int x, int y, char symbol) : base(x, y, symbol) { }
        public override void ResetFields(Maze maze)
        {
            X = 1;
            Y = maze.Width - 2;
        }
        public override void Act(Maze maze)
        {
            MoveRandom(maze);
        }
    }
}
