using MazeRogueLike.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MazeRogueLike
{
    internal static class GameUpdater
    {
        public static void Update(Maze maze,Player player) //обновляем состояние игры, пока игрок не дойдет до выхода или не умрет
        {
            do
            {
                Renderer.PrintMaze(maze);
                player.Move(maze);
                
                //ActionManager.MovePlayer(maze, zombie, player);
                //ActionManager.MoveZombie(maze, zombie, player);
                //ActionManager.MoveArcherOrArrow(maze, archer, player);
            } while (player.IsAlive && !player.IsEscaped);

            player.ResetFields();

            //while (player.IsAlive && !player.IsEscaped) ;

            /*ResetAllFields();*/ //Сброс полей значимых объектов на карте до дефолтного состояния
        }

        //private static void ResetAllFields()
        //{
            
        //    //zombie.ResetZombieFields(maze);
        //    //archer.ResetArcherFields(maze);
        //}
    }
}
