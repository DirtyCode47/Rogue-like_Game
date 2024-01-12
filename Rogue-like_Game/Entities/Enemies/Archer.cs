using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue_like_Game.MazeLogic;
using Rogue_like_Game.MazeLogic.Render;
using Rogue_like_Game.Entities.Players;

namespace Rogue_like_Game.Entities.Enemies
{
    internal class Archer : Enemy
    {
        public Arrow arrow;
        public Archer(int x, int y, char symbol) : base(x, y, symbol)
        {
            arrow = new Arrow(0, 0, '-');
        }
        public override void ResetFields(Maze maze)
        {
            X = maze.Height - 2;
            Y = maze.Width - 2;
            arrow.ResetFields(maze);
        }
        public override void Act(Maze maze, Dictionary<string, Entity> acting_entities)
        {
            Player player = (Player)acting_entities["Player"];

            if (IsNearbyOtherEntity(player)) //Если рядом со стрелой игрок, то он умирает
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }

            (int delta_x, int delta_y, bool is_visible) = IsPlayerVisibleOnSameAxis(maze, player);

            if (!arrow.IsInAir)  //Если стела не летит в настоящее время
            {
                if (!is_visible)
                {
                    MoveRandom(maze); //Если игрока не видно, то рандомное движение
                }
                else
                {
                    arrow.SetFieldsForFlightCondition(maze, X, delta_x, Y, delta_y); //Иначе лучник пускает стрелу, у которой устанавливаются значения полей
                }
            }
            else
            {
                arrow.Act(maze, acting_entities);
            }
        }
    }
}
