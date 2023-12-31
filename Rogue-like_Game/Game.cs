using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Game
    {
        private Maze maze;
        private Player player;
        private Zombie zombie;
        public Game() 
        {
            //координаты так проставлены в рамках типо "геймдизайна", можно менять только размер лабиринта.
            //Но в качестве передаваемых параметров в лабиринт должны быть не совсем маленькие
            //и одинаковые нечетные числа.
            maze = new Maze(11,11); //в конструктор передаем размер лабиринта. Обязательно нечетное число!!!  23/23 - топ
            player = new Player(1,1); //координаты спавна игрока
            zombie = new Zombie(1,maze.width-2); //координаты спавна зомби
        }
        public void Run()
        {
            do
            {
                CreateMaze(maze);
                Update();

                Console.WriteLine("Do you want to play again? (y/n)");
            } while (Console.ReadKey(true).Key == ConsoleKey.Y);
        }
        private void CreateMaze(Maze maze)
        {
            MazeManager.InitializeMaze(maze);
            MazeManager.GenerateMaze(maze, 1, 1);
            MazeManager.SpawnEnemies(maze,zombie);
        }
        private void Update() 
        {
            do
            {
                Renderer.PrintMaze(maze);
                ActionManager.MovePlayer(maze, zombie, player);
                ActionManager.MoveZombie(maze, zombie, player);
            } while (player.IsAlive && !player.IsEscaped);

            ResetAllFields();
        }
        
        private void ResetAllFields()
        {
            player.ResetPlayerFields();
            zombie.ResetZombieFields(maze);
        }
    }
}
