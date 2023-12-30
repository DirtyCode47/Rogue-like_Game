using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal static class MoveManager
    {
        //private Player player;
        //public MoveManager(char[,] maze, Player player)
        //{
        //    this.player = player;
        //}
        public static void MovePlayer(Maze maze,Player player)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W:
                    TryMove(maze, player, -1, 0);
                    break;
                case ConsoleKey.S:
                    TryMove(maze, player, 1, 0);
                    break;
                case ConsoleKey.A:
                    TryMove(maze, player, 0, -1);
                    break;
                case ConsoleKey.D:
                    TryMove(maze, player, 0, 1);
                    break;
            }

            if(player.X == maze.width - 2 && player.Y == maze.height - 1) 
            {
                player.IsEscaped = true;
            }
        }
        private static void TryMove(Maze maze, Entity entity, int deltaX, int deltaY)
        {
            int newX = entity.X + deltaX;
            int newY = entity.Y + deltaY;

            if (IsInBounds(maze, newX, newY) && (maze.map[newX, newY] == ' ' || maze.map[newX, newY] == 'E'))
            {
                maze.map[entity.X, entity.Y] = ' '; // Освобождаем текущую клетку
                entity.X = newX;
                entity.Y = newY;
                maze.map[entity.X, entity.Y] = entity.Symbol; // Помещаем сущность в новую клетку
            }
        }

        private static bool IsInBounds(Maze maze,int x, int y)
        {
            return x >= 0 && x < maze.width && y >= 0 && y < maze.height;
        }
    }
}
