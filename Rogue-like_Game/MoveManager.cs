using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static void MovePlayer(Maze maze,Zombie zombie,Player player)
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

            if ((player.X - zombie.X == 0 && Math.Abs(player.Y - zombie.Y) == 1) || (player.Y - zombie.Y == 0 && Math.Abs(player.X - zombie.X) == 1))
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }
            if(player.X == maze.width - 2 && player.Y == maze.height - 1) 
            {
                player.IsEscaped = true;
            }
        }

        public static void MoveZombie(Maze maze, Zombie zombie, Player player)
        {
            //bool CheckIfVisible()
            //{
            //    return true;
            //}

            if (IsInMeleeAtackRange(player,zombie))
            {
                return;
            }

            var random = new Random();

            bool is_visible_x = false; //Види
            bool is_visible_y = false;

            if (player.X == zombie.X) 
            {
                if(player.Y < zombie.Y)
                {
                    for(int i=player.Y+1;i<zombie.Y;i++)
                    {
                        if (maze.map[zombie.X,i] != ' ' ) 
                        {
                            is_visible_x = false;
                            break;
                        }
                        is_visible_x = true; //Код дойдет до сюда, если зомби увидел игрока слева или справа (Нет стен между игроком и зомби)
                    }
                }
                else 
                {
                    for (int i = zombie.Y+1; i < player.Y; i++)
                    {
                        if (maze.map[zombie.X, i] != ' ')
                        {
                            is_visible_x=false;
                            break;
                        }
                        is_visible_x = true;
                    }
                }
            }
            if(player.Y == zombie.Y)
            {
                if (player.X < zombie.X)
                {
                    for (int i = player.X+1; i < zombie.X; i++)
                    {
                        if (maze.map[i, zombie.Y] != ' ')
                        {
                            is_visible_y = false;
                            break;
                        }
                        is_visible_y = true; //Код дойдет до сюда, если зомби увидел игрока снизу или сверху (Нет стен между игроком и зомби)
                    }
                }
                else
                {
                    for (int i = zombie.X + 1; i < player.X; i++)
                    {
                        if (maze.map[i, zombie.Y] != ' ')
                        {
                            is_visible_y = false;
                            break;
                        }
                        is_visible_y = true;
                    }
                }
            }


            
            if(is_visible_x)
            {
                if(player.Y < zombie.Y)
                {
                    //maze.map[zombie.X, zombie.Y] = ' ';
                    //zombie.Y--;
                    TryMove(maze, zombie, 0, -1);

                    if (IsInMeleeAtackRange(player, zombie))
                    {
                        Renderer.PrintMaze(maze);
                        player.IsAlive = false;
                    }
                }

                if(player.Y > zombie.Y)
                {
                    //maze.map[zombie.X, zombie.Y] = ' ';
                    //zombie.Y++;
                    TryMove(maze, zombie, 0, 1);

                    if (IsInMeleeAtackRange(player, zombie))
                    {
                        Renderer.PrintMaze(maze);
                        player.IsAlive = false;
                    }
                }
            }
            else if(is_visible_y)
            {
                if(player.X < zombie.X)
                {
                    //maze.map[zombie.X, zombie.Y] = ' ';
                    //zombie.X--;
                    TryMove(maze, zombie, -1, 0);

                    if (IsInMeleeAtackRange(player, zombie))
                    {
                        Renderer.PrintMaze(maze);
                        player.IsAlive = false;
                    }
                }
                if (player.X > zombie.X) 
                {
                    //maze.map[zombie.X, zombie.Y] = ' ';
                    //zombie.X++;
                    TryMove(maze, zombie, 1, 0);

                    if (IsInMeleeAtackRange(player, zombie))
                    {
                        Renderer.PrintMaze(maze);
                        player.IsAlive = false;
                    }
                }
            }
            else
            {
                int delta_x, delta_y;
                if (random.NextDouble() > 0.4) //Рандомное перемещение
                {
                    bool is_moved = false;
                    while (!is_moved)
                    {
                        int random_direction = random.Next(4);
                        (delta_x, delta_y) = random_direction switch
                        {
                            0 => (-1, 0),
                            1 => (1, 0),
                            2 => (0, -1),
                            3 => (0, 1),
                            _ => (0, 0)
                        };
                        is_moved = TryMove(maze, zombie, delta_x, delta_y);
                    }
                }
                if (IsInMeleeAtackRange(player, zombie))
                {
                    Renderer.PrintMaze(maze);
                    player.IsAlive = false;
                }
            }
        }

        private static bool IsInMeleeAtackRange(Player player, Zombie zombie)
        {
            return (player.X - zombie.X == 0 && Math.Abs(player.Y - zombie.Y) == 1) || (player.Y - zombie.Y == 0 && Math.Abs(player.X - zombie.X) == 1);
        }

        private static bool TryMove(Maze maze, Entity entity, int deltaX, int deltaY)
        {
            bool is_moved = false;
            int newX = entity.X + deltaX;
            int newY = entity.Y + deltaY;

            if (IsInBounds(maze, newX, newY) && (maze.map[newX, newY] == ' ' || maze.map[newX, newY] == 'E'))
            {
                maze.map[entity.X, entity.Y] = ' '; // Освобождаем текущую клетку
                entity.X = newX;
                entity.Y = newY;
                maze.map[entity.X, entity.Y] = entity.Symbol; // Помещаем сущность в новую клетку
                is_moved = true;
            }
            return is_moved;
        }

        private static bool IsInBounds(Maze maze,int x, int y)
        {
            return x >= 0 && x < maze.width && y >= 0 && y < maze.height;
        }
    }
}
