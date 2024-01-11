using Rogue_like_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Archer:Enemy
    {
        public Archer(int x, int y, char symbol) : base(x, y, symbol) { }
        public override void ResetFields(Maze maze)
        {
            X = maze.Height - 2;
            Y = maze.Width - 2;
        }
        public override void Act(Maze maze,Dictionary<string, Entity> acting_entities)
        {
            if()
            MoveRandom(maze);
        }
    }
}
