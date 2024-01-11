using Rogue_like_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Zombie:Enemy
    {
        public Zombie(int x, int y, char symbol) : base(x, y, symbol) { }
        public override void ResetFields(Maze maze)
        {
            X = 1;
            Y = maze.Width - 2;
        }
        public override void Act(Maze maze,Dictionary<string,Entity> acting_entities)
        {
            Player player = (Player)acting_entities["Player"];

            if(IsNearbyOtherEntity(player))
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }

            (int delta_x,int delta_y,bool is_visible) = IsPlayerVisibleOnSameAxis(maze,player);

            if(!is_visible)
            {
                MoveRandom(maze);
            }
            else
            {
                MoveToPlayer(maze, delta_x, delta_y);
            }

            if (IsNearbyOtherEntity(player))
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }
        }
    }
}
