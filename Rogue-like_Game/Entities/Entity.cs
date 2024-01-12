using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Rogue_like_Game.MazeLogic;

namespace Rogue_like_Game.Entities
{
    internal abstract class Entity
    {
        private int x;
        private int y;
        private char symbol;

        public Entity(int x, int y, char symbol)
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }
        public char Symbol
        {
            get => symbol;
            set => symbol = value;
        }

        protected bool IsNearbyOtherEntity(Entity entity) //Находится ли рядом с сущностью другая сущность
        {
            return (X - entity.X == 0 && Math.Abs(Y - entity.Y) == 1) || (Y - entity.Y == 0 && Math.Abs(X - entity.X) == 1);
        }

        public abstract void ResetFields(Maze maze);  //Сбросить все поля игровой сущности в состояние начала игры
        public abstract void Act(Maze maze,Dictionary<string,Entity> entities);  //Действие сущности за один ход
    }
}
