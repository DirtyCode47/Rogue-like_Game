using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Arrow:Entity
    {
        public Arrow()
        {
            X = 0;
            Y = 0;
        }
        
        public bool IsExist = false;
        

        public void ResetArrowFields()
        {
            IsExist = false;
        }
    }
}
