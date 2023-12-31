using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Archer:Entity
    {
        public Archer(int x,int y):base(x,y)
        {
            Symbol = 'A';
        }
        public void ResetArcherFields()
        {

        }
    }
}
