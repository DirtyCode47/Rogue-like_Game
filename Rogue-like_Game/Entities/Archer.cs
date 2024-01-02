using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Archer:Entity
    {
        public Arrow arrow;
        public Archer(int x,int y):base(x,y)
        {
            Symbol = 'A';
            arrow = new Arrow();
        }
        public void ResetArcherFields(Maze maze)
        {
            X = maze.height - 2;
            Y = maze.width - 2;
            arrow.ResetArrowFields();
        }
    }
}
