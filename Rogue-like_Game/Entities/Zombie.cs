using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Zombie:Entity
    {
        public Zombie(int x,int y):base(x,y) 
        {
            Symbol = 'Z';
        }
        public void ResetZombieFields(Maze maze)
        {
            X = 1;
            Y = maze.width-2;
        }
    }
}
