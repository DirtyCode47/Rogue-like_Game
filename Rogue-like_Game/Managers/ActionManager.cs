using Rogue_like_Game.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal static class ActionManager
    {
        public static void MovePlayer(Maze maze, Zombie zombie, Player player)
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

            if (IsInMeleeAtackRange(player, zombie))
            {
                Renderer.PrintMaze(maze);
                player.IsAlive = false;
            }
            if (player.X == maze.width - 2 && player.Y == maze.height - 1) //Если игрок нашел выход
            {
                player.IsEscaped = true;
            }
        }

        public static void MoveZombie(Maze maze, Zombie zombie, Player player)
        {
            if (IsInMeleeAtackRange(player, zombie))
            {
                player.IsAlive = false;
                return;
            }

            var random = new Random();

            bool is_visible_x = false;
            bool is_visible_y = false;

            if (player.X == zombie.X)  //в данном блоке проверка, видит ли зомби игрока справа или слева
            {
                if (player.Y < zombie.Y)
                {
                    is_visible_x = IsVisibleOnSameX(player, zombie);
                }
                else
                {
                    is_visible_x = IsVisibleOnSameX(zombie, player);
                }
            }


            if (player.Y == zombie.Y) //в данном блоке проверка, видит ли зомби игрока снизу или сверху
            {
                if (player.X < zombie.X)
                {
                    is_visible_y = IsVisibleOnSameY(player, zombie);
                }
                else
                {
                    is_visible_y = IsVisibleOnSameY(zombie, player);
                }
            }

            if (is_visible_x) //Движется в сторону игрока вправо или влево, если его видит с одной из этих сторон
            {
                if (player.Y < zombie.Y)
                {
                    TryMove(maze, zombie, 0, -1);
                    ZombieTryAttack();
                }

                if (player.Y > zombie.Y)
                {
                    TryMove(maze, zombie, 0, 1);
                    ZombieTryAttack();
                }
            }

            else if (is_visible_y)  //Движется в сторону игрока вверх или вниз, если его видит с одной из этих сторон
            {
                if (player.X < zombie.X)
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

        public static void MoveArcherOrArrow(Maze maze, Archer archer, Player player)
        {

            if (archer.arrow.IsExist) //Если стрела существует, перемещаем её вплоть до стены, либо игрока. 
            {                         //А стрелок в это время неподвижен
                if (archer.arrow.X == archer.X)
                {
                    if (archer.arrow.Y < archer.Y) //Отдаляем стрелу от стрелка по Y
                    {
                        bool is_moved = TryMove(maze, archer.arrow, 0, -1);

                        if (player.X == archer.arrow.X && archer.arrow.Y - player.Y <= 1) //Если стрела находится в соседней клетке
                        {                                                                 //То она как бы уже кончиком пронзает игрока
                            player.IsAlive = false;                                       //Так что игрок умирает
                            Renderer.PrintMaze(maze);
                        }

                        if (!is_moved)
                        {
                            maze.map[archer.arrow.X, archer.arrow.Y] = ' ';
                            archer.arrow.ResetArrowFields();
                        }

                        return;
                    }
                    else
                    {
                        bool is_moved = TryMove(maze, archer.arrow, 0, 1);

                        if (player.X == archer.arrow.X && player.Y - archer.arrow.Y <= 1)
                        {
                            player.IsAlive = false;
                            Renderer.PrintMaze(maze);
                        }
                        if (!is_moved)
                        {
                            maze.map[archer.arrow.X, archer.arrow.Y] = ' ';
                            archer.arrow.ResetArrowFields();
                        }

                        return;
                    }
                }

                if (archer.arrow.Y == archer.Y)
                {
                    if (archer.arrow.X < archer.X) //Отдаляем стрелу от стрелка по X
                    {
                        bool is_moved = TryMove(maze, archer.arrow, -1, 0);
                        
                        if (player.Y == archer.arrow.Y && archer.arrow.X - player.X <= 1)
                        {
                            player.IsAlive = false;
                            Renderer.PrintMaze(maze);
                        }
                        if (!is_moved)
                        {
                            maze.map[archer.arrow.X, archer.arrow.Y] = ' ';
                            archer.arrow.ResetArrowFields();
                        }

                        return;
                    }
                    else
                    {
                        bool is_moved = TryMove(maze, archer.arrow, 1, 0);
                        
                        if (player.Y == archer.arrow.Y && player.X - archer.arrow.X <= 1)
                        {
                            player.IsAlive = false;
                            Renderer.PrintMaze(maze);
                        }
                        if (!is_moved)
                        {
                            maze.map[archer.arrow.X, archer.arrow.Y] = ' ';
                            archer.arrow.ResetArrowFields();
                        }

                        return;
                    }
                }
            }


            bool is_visible_x = false;
            bool is_visible_y = false;

            if (player.X == archer.X)  //в данном блоке проверка, видит ли лучник игрока справа или слева
            {
                if (player.Y < archer.Y)
                {
                    is_visible_x = IsVisibleOnSameX(player, archer);
                }
                else
                {
                    is_visible_x = IsVisibleOnSameX(archer, player);
                }
            }


            if (player.Y == archer.Y) //в данном блоке проверка, видит ли лучник игрока снизу или сверху
            {
                if (player.X < archer.X)
                {
                    is_visible_y = IsVisibleOnSameY(player, archer);
                }
                else
                {
                    is_visible_y = IsVisibleOnSameY(archer, player);
                }
            }





            if (is_visible_x)   //Логика движения лучника, а также спавн стрелы, если лучник увидел игрока
            {
                archer.arrow.Symbol = '-';
                if (archer.Y > player.Y)
                {
                    archer.arrow.X = archer.X;
                    archer.arrow.Y = archer.Y;

                    TryMove(maze, archer.arrow, 0, -1);
                }
                else
                {
                    archer.arrow.X = archer.X;
                    archer.arrow.Y = archer.Y;

                    TryMove(maze, archer.arrow, 0, 1);
                }
                archer.arrow.IsExist = true;
            }
            else if (is_visible_y)
            {
                archer.arrow.Symbol = '|';
                if (archer.X > player.X)
                {
                    archer.arrow.X = archer.X;
                    archer.arrow.Y = archer.Y;

                    TryMove(maze, archer.arrow, -1, 0);
                }
                else
                {
                    archer.arrow.X = archer.X;
                    archer.arrow.Y = archer.Y;

                    TryMove(maze, archer.arrow, 1, 0);
                }
                archer.arrow.IsExist = true;
            }
            else
            {
                var random = new Random();
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
                        is_moved = TryMove(maze, archer, delta_x, delta_y);
                    }
                }
            }

            bool IsVisibleOnSameX(Entity entity_1, Entity entity_2)
            {
                for (int i = entity_1.Y + 1; i < entity_2.Y; i++)
                {
                    if (maze.map[archer.X, i] != ' ')
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
                    if (maze.map[i, archer.Y] != ' ')
                    {
                        return false;
                    }
                }
                return true; //Код дойдет до сюда, если зомби может видеть игрока снизу или сверху (Нет стен между игроком и зомби)
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

            if (entity is Player && maze.map[newX, newY] == 'E')
            {
                maze.map[entity.X, entity.Y] = ' ';
                MoveToNewPosition();
                is_moved = true;
                return is_moved;
            }

            if (entity is Arrow && IsInBounds(maze, newX, newY) && maze.map[newX, newY] == ' ')
            {
                if (maze.map[newX - deltaX, newY - deltaY] == 'A')
                {
                    MoveToNewPosition();
                    is_moved = true;
                }
            }

            if (IsInBounds(maze, newX, newY) && maze.map[newX, newY] == ' ')
            {
                maze.map[entity.X, entity.Y] = ' '; // Освобождаем текущую клетку
                MoveToNewPosition();
                is_moved = true;
            }
            return is_moved;

            void MoveToNewPosition()
            {
                entity.X = newX;
                entity.Y = newY;
                maze.map[entity.X, entity.Y] = entity.Symbol; // Помещаем сущность в новую клетку
            }
        }

        private static bool IsInBounds(Maze maze, int x, int y)
        {
            return x >= 0 && x < maze.width && y >= 0 && y < maze.height;
        }
    }
}
