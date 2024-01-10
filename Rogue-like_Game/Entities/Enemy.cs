using MazeRogueLike;
using MazeRogueLike.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal abstract class Enemy : Entity
    {
        public Enemy(int x, int y, char symbol) : base(x, y, symbol) {}

        public void MoveRandom(Maze maze)
        {
            var random = new Random(Guid.NewGuid().GetHashCode()); //Вроде как более рандомно

            var all_directions = new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var available_directions_to_move = new List<(int, int)>();

            foreach (var direction in all_directions) 
            {
                if(maze.Map[X+direction.Item1,Y+direction.Item2] == ' ')
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
    }
}
