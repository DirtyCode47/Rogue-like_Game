using Rogue_like_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Archer:Enemy
    {
        public Arrow arrow;
        public Archer(int x, int y, char symbol) : base(x, y, symbol) 
        {
            arrow = new Arrow(0,0,'-');
        }
        public override void ResetFields(Maze maze)
        {
            X = maze.Height - 2;
            Y = maze.Width - 2;
            arrow.ResetFields(maze);
        }
        public override void Act(Maze maze,Dictionary<string, Entity> acting_entities)
        {
            Player player = (Player)acting_entities["Player"];

            if (IsNearbyOtherEntity(player))
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }

            (int delta_x, int delta_y, bool is_visible) = IsPlayerVisibleOnSameAxis(maze, player); 
            
            if (!arrow.IsInAir)
            {
                if (!is_visible)
                {
                    MoveRandom(maze);
                }
                else
                {
                    arrow.SetFieldsForFlightCondition(maze, X, delta_x, Y, delta_y);
                }
            }
            else
            {
                arrow.Act(maze,acting_entities);
            }
        }
    }
}
