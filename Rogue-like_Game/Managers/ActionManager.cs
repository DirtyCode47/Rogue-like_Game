using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal static class ActionManager
    {
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

            if(IsInMeleeAtackRange(player,zombie))
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }
            if(player.X == maze.width - 2 && player.Y == maze.height - 1) //Если игрок нашел выход
            {
                player.IsEscaped = true;
            }
        }

        public static void MoveZombie(Maze maze, Zombie zombie, Player player)
        {
            if (IsInMeleeAtackRange(player,zombie))
            {
                player.IsAlive=false;
                return;
            }


            var random = new Random();

            bool is_visible_x = false; 
            bool is_visible_y = false;

            if (player.X == zombie.X)  //в данном блоке проверка, видит ли зомби игрока справа или слева
            {
                if(player.Y < zombie.Y)
                {     
                    is_visible_x = IsVisibleOnSameX(player,zombie); 
                }
                else 
                {
                    is_visible_x = IsVisibleOnSameX(zombie,player);
                }
            }


            if(player.Y == zombie.Y) //в данном блоке проверка, видит ли зомби игрока снизу или сверху
            {
                if (player.X < zombie.X)
                {
                    is_visible_y = IsVisibleOnSameY(player,zombie);
                }
                else
                {
                    is_visible_y= IsVisibleOnSameY(zombie, player);
                }
            }

            if(is_visible_x) //Движется в сторону игрока вправо или влево, если его видит с одной из этих сторон
            {
                if(player.Y < zombie.Y)
                {
                    TryMove(maze, zombie, 0, -1);
                    ZombieTryAttack();
                }

                if(player.Y > zombie.Y)
                {
                    TryMove(maze, zombie, 0, 1);
                    ZombieTryAttack();
                }
            }

            else if(is_visible_y)  //Движется в сторону игрока вверх или вниз, если его видит с одной из этих сторон
            {
                if(player.X < zombie.X)
                {
                    TryMove(maze, zombie, -1, 0);
                    ZombieTryAttack();
                }
                if (player.X > zombie.X) 
                {
                    TryMove(maze, zombie, 1, 0);
                    ZombieTryAttack();
                }
            }

            else  //Рандомное перемещение с вероятностью 80%, если зомби не видит игрока
            {
                int delta_x, delta_y;
                if (random.NextDouble() > 0.2) 
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
                ZombieTryAttack();
            }


            bool IsVisibleOnSameX(Entity entity_1, Entity entity_2)
            {
                for (int i = entity_1.Y + 1; i < entity_2.Y; i++)
                {
                    if (maze.map[zombie.X, i] != ' ')
                    {
                        return false;
                    }
                }
                return true; //Код дойдет до сюда, если зомби может видеть игрока слева или справа (Нет стен между игроком и зомби)
            }


            bool IsVisibleOnSameY(Entity entity_1, Entity entity_2)
            {
                for (int i = entity_1.X + 1; i < entity_2.X; i++)
                {
                    if (maze.map[i, zombie.Y] != ' ')
                    {
                        return false;
                    }
                }
                return true; //Код дойдет до сюда, если зомби может видеть игрока снизу или сверху (Нет стен между игроком и зомби)
            }


            void ZombieTryAttack()
            {
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
