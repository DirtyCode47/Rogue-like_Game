using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MazeRogueLike.Entities
{
    internal class Player:Entity
    {
        private bool is_alive;
        private bool is_escaped;
        public Player(int x,int y,char symbol):base(x,y,symbol)
        {
            is_alive = true;
            is_escaped = false;
        }

        public bool IsAlive 
        {
            get => is_alive;
            set => is_alive = value;
        }
        public bool IsEscaped
        {
            get => is_escaped;
            set => is_escaped = value;
        }

        public override void ResetFields()
        {
            X = 1;
            Y = 1;
            IsAlive = true;
            IsEscaped = false;
        }

        public override void Move(Maze maze)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W:
                    TryMove(maze, -1, 0);
                    break;
                case ConsoleKey.S:
                    TryMove(maze, 1, 0);
                    break;
                case ConsoleKey.A:
                    TryMove(maze, 0, -1);
                    break;
                case ConsoleKey.D:
                    TryMove(maze, 0, 1);
                    break;
            }
            if (X == maze.Width - 2 && Y == maze.Width - 1) //Если игрок нашел выход
            {
                IsEscaped = true;
            }

            bool TryMove(Maze maze, int deltaX, int deltaY)
            {
                bool is_moved = false;
                int newX = X + deltaX;
                int newY = Y + deltaY;

                if (maze.Map[newX, newY] == 'E')
                {
                    maze.Map[X,Y] = ' ';
                    MoveToNewPosition();
                    is_moved = true;
                    return is_moved;
                }

                if (IsInBounds(maze, newX, newY) && maze.Map[newX, newY] == ' ')
                {
                    maze.Map[X, Y] = ' '; // Освобождаем текущую клетку
                    MoveToNewPosition();
                    is_moved = true;
                }
                return is_moved;

                void MoveToNewPosition()
                {
                    X = newX;
                    Y = newY;
                    maze.Map[X, Y] = Symbol; // Помещаем сущность в новую клетку
                }
            }
        }
    }
}
