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
    internal class Zombie : Enemy
    {
        public Zombie(int x, int y, char symbol) : base(x, y, symbol) { }
        public override void ResetFields(Maze maze)
        {
            X = 1;
            Y = maze.Width - 2;
        }
        public override void Act(Maze maze, Dictionary<string, Entity> acting_entities)
        {
            Player player = (Player)acting_entities["Player"];

            if (IsNearbyOtherEntity(player))  //Если игрок сам подошел к зомби в упор, то зомби его убивает 
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }

            (int delta_x, int delta_y, bool is_visible) = IsPlayerVisibleOnSameAxis(maze, player);

            if (!is_visible) //Если зомюи не видит игрока, то зомби двигается рандомно, иначе - прямиком к игроку
            {
                MoveRandom(maze);
            }
            else
            {
                MoveToPlayer(maze, delta_x, delta_y);
            }

            if (IsNearbyOtherEntity(player))  //Если зомби приблизился к игроку в упор, то зомби убивает игрока
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }
        }
    }
}
