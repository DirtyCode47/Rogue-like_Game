using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.Entities
{
    internal class Arrow:Entity
    {
        public Arrow(int x,int y,char symbol):base(x,y,symbol)
        {
            IsInAir = false;
            X = 0; 
            Y = 0;
            Symbol = '-';
            DirectionToMove = (0, 0);
        }
        public bool IsInAir { get; set; }
        public (int, int) DirectionToMove { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }

        public void SetFieldsForFlightCondition(Maze maze, int x, int delta_x, int y, int delta_y)
        {
            IsInAir = true;
            Symbol = (delta_x != 0) ? '|' : '-';
            DirectionToMove = (delta_x, delta_y);

            X = x+delta_x;
            Y = y+delta_y;
            maze.Map[X, Y] = Symbol;
        }
        public override void Act(Maze maze, Dictionary<string, Entity> acting_entities)
        {
            Player player = (Player)acting_entities["Player"];


            //int prev_x = X;
            //int prev_y = Y;
            //int awaited_x = X + DirectionToMove.Item1;
            //int awaited_y = Y + DirectionToMove.Item1;

            if (maze.Map[X + DirectionToMove.Item1, Y + DirectionToMove.Item2] == ' ') //Если есть куда лететь дальше
            {
                maze.Map[X, Y] = ' ';

                X += DirectionToMove.Item1;
                Y += DirectionToMove.Item2;

                maze.Map[X, Y] = Symbol;
                return;
            }

            if(maze.Map[X + DirectionToMove.Item1, Y + DirectionToMove.Item2] != 'P') //Если стена, или символ входа или выхода
            {
                maze.Map[X, Y] = ' ';
                ResetFields(maze);
                return;
            }

            player.IsAlive = false;
            Renderer.PrintMaze(maze);
        }
        public override void ResetFields(Maze maze)
        {
            IsInAir = false;
            DirectionToMove = (0, 0);
            Symbol = '-';
            X = 0;
            Y = 0;
        }
    }
}
