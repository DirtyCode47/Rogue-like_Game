﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Rogue_like_Game.MazeLogic.Render;
using Rogue_like_Game.MazeLogic;

namespace Rogue_like_Game.Entities.Players
{
    internal class Player : Entity
    {
        private bool is_alive; //Жив ли игрок
        private bool is_escaped; //Нашел ли игрок выход на потенциально следующий уровень

        public Player(int x, int y, char symbol) : base(x, y, symbol)
        {
            is_alive = true;
            is_escaped = false;
        }

        public bool IsAlive
        {
            get => is_alive;
            set => is_alive = value;
        }
        public bool IsEscaped
        {
            get => is_escaped;
            set => is_escaped = value;
        }

        public override void ResetFields(Maze maze)
        {
            X = 1;
            Y = 1;
            IsAlive = true;
            IsEscaped = false;
        }

        public override void Act(Maze maze, Dictionary<string, Entity> acting_entities)
        {
            var zombie = acting_entities["Zombie"];
            var archer = acting_entities["Archer"];

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W:
                    TryMove(maze, -1, 0);
                    break;
                case ConsoleKey.S:
                    TryMove(maze, 1, 0);
                    break;
                case ConsoleKey.A:
                    TryMove(maze, 0, -1);
                    break;
                case ConsoleKey.D:
                    TryMove(maze, 0, 1);
                    break;
            }
            if (X == maze.Width - 2 && Y == maze.Width - 1) //Если игрок нашел выход
            {
                IsEscaped = true;
            }

            if (IsNearbyOtherEntity(zombie) || IsNearbyOtherEntity(archer))
            {
                Renderer.PrintMaze(maze);
                IsAlive = false; //Игрок умирает, если в упоре как мечник, так и лучник, у которого на всякий случай есть нож в кармане
            }



            bool TryMove(Maze maze, int deltaX, int deltaY) //Может ли игрок пройти в том направлении, куда нажал клавишу
            {
                bool is_moved = false;
                int newX = X + deltaX;
                int newY = Y + deltaY;

                if (maze.Map[newX, newY] == 'E')
                {
                    maze.Map[X, Y] = ' ';
                    MoveToNewPosition();
                    is_moved = true;
                    return is_moved;
                }

                if (maze.Map[newX, newY] == ' ')
                {
                    maze.Map[X, Y] = ' '; // Освобождаем текущую клетку
                    MoveToNewPosition();
                    is_moved = true;
                }
                return is_moved;

                void MoveToNewPosition()
                {
                    X = newX;
                    Y = newY;
                    maze.Map[X, Y] = Symbol; // Помещаем сущность в новую клетку
                }
            }
        }
    }
}
