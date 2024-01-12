using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Rogue_like_Game.MazeLogic;
using Rogue_like_Game.Entities.Players;

namespace Rogue_like_Game.Entities.Enemies
{
    internal abstract class Enemy : Entity
    {
        public Enemy(int x, int y, char symbol) : base(x, y, symbol) { }

        public void MoveRandom(Maze maze) //Рандомное движение врагов
        {
            var random = new Random(Guid.NewGuid().GetHashCode()); //Лучше чем new Random()

            var all_directions = new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var available_directions_to_move = new List<(int, int)>();

            foreach (var direction in all_directions)
            {
                if (maze.Map[X + direction.Item1, Y + direction.Item2] == ' ')
                {
                    available_directions_to_move.Add(direction);
                }
            }

            if (available_directions_to_move.Count == 0)
            {
                return;
            }
            var final_direction = available_directions_to_move[random.Next(available_directions_to_move.Count)];

            maze.Map[X, Y] = ' ';

            X += final_direction.Item1;
            Y += final_direction.Item2;

            maze.Map[X, Y] = Symbol;
        }

        public void MoveToPlayer(Maze maze, int delta_x, int delta_y) //Движение в сторону игрока
        {
            maze.Map[X, Y] = ' ';

            X += delta_x;
            Y += delta_y;

            maze.Map[X, Y] = Symbol;
        }


        public (int, int, bool) IsPlayerVisibleOnSameAxis(Maze maze, Player player) //Возвращает кортеж 
        {                                              //Со значениями Delta_x и Delta_y для дальнейшего движения в эту сторону
            int deltaX = 0, deltaY = 0;                //и с флагом, который указывает на то, виден ли игрок
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

            if (Y == player.Y)
            {
                int direction = Math.Sign(player.X - X);
                for (int i = X + direction; i != player.X; i += direction)
                {
                    if (maze.Map[i, Y] != ' ')
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
