using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }

        public Entity(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
}
