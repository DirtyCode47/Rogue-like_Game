﻿using Rogue_like_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal abstract class Enemy : Entity
    {
        public Enemy(int x, int y, char symbol) : base(x, y, symbol) { }

        public void MoveRandom(Maze maze)
        {
            var random = new Random(Guid.NewGuid().GetHashCode()); //Вроде как лучше чем просто new Random()

            var all_directions = new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var available_directions_to_move = new List<(int, int)>();

            foreach (var direction in all_directions)
            {
                if (maze.Map[X + direction.Item1, Y + direction.Item2] == ' ')
                {
                    available_directions_to_move.Add(direction);
                }
            }

            var final_direction = available_directions_to_move[random.Next(available_directions_to_move.Count)];

            maze.Map[X, Y] = ' ';

            X += final_direction.Item1;
            Y += final_direction.Item2;

            maze.Map[X, Y] = Symbol;
        }

        public void MoveToPlayer(Maze maze, int delta_x, int delta_y)
        {
            maze.Map[X, Y] = ' ';

            X += delta_x;
            Y += delta_y;

            maze.Map[X, Y] = Symbol;
        }


        public (int, int, bool) IsPlayerVisibleOnSameAxis(Maze maze, Player player)
        {
            int deltaX = 0, deltaY = 0;
            bool isVisible = false;

            if (X == player.X)
            {
                int direction = Math.Sign(player.Y - Y);
                for (int i = Y + direction; i != player.Y; i += direction)
                {
                    if (maze.Map[X, i] != ' ')
                    {
                        isVisible = false;
                        break;
                    }
                    isVisible = true;
                    deltaY = direction;
                }
            }
            else 
            {
                int direction = Math.Sign(player.X - X);
                for (int i = X + direction; i != player.X; i += direction)
                {
                    if (maze.Map[Y, i] != ' ')
                    {
                        isVisible = false;
                        break;
                    }
                    isVisible = true;
                    deltaX = direction;
                }
            }

            return (deltaX, deltaY, isVisible);
        }
    }
}
