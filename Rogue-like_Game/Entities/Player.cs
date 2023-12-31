﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Player:Entity
    {
        public Player(int x,int y):base(x,y)
        {
            Symbol = 'P';
            IsAlive = true;
            IsEscaped = false;
        }
        public bool IsAlive { get; set; }
        public bool IsEscaped { get; set; }

        public void ResetPlayerFields()
        {
            X = 1;
            Y = 1;
            IsEscaped = false;
            IsAlive = true;
        }
    }
}
