﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

        protected bool IsNearbyOtherEntity(Entity entity)
        {
            return (X - entity.X == 0 && Math.Abs(Y - entity.Y) == 1) || (Y - entity.Y == 0 && Math.Abs(X - entity.X) == 1);
        }

        protected bool IsInBounds(Maze maze,int x,int y)
        {
            return x >= 0 && x < maze.Width && y >= 0 && y < maze.Height;
        }
        //public Entity(int x,int y)
        //{
        //    this.x = x;
        //    this.y = y;
        //}

        public abstract void ResetFields(Maze maze);  //Сбросить все поля игровой сущности в состояние начала игры
        public abstract void Act(Maze maze,Dictionary<string,Entity> entities);  //Действие за один ход
        //public abstract bool TryMove(Maze maze, int deltaX, int deltaY);
        //public abstract bool IsInBounds(Maze maze, int x, int y);

        //public Entity() { }
    }
}
