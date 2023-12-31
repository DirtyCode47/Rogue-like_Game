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
            maze = new Maze(11,11); //в конструктор передаем размер лабиринта. Обязательно нечетное число!!! 23/23
            player = new Player(1,1); //координаты спавна игрока
            zombie = new Zombie(1,maze.width-2);
        }
        public void Run()
        {
            do
            {
                CreateMaze(maze);
                Update();
                player.ResetPlayerFields();
                zombie.ResetZombieFields(maze);
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

                //PrintMaze();
                //PlacePlayer();
                //PlaceMeleeEnemy();
                //PlaceArcherEnemy();
                //RunGame();
                Renderer.PrintMaze(maze);
                ActionManager.MovePlayer(maze, zombie, player);
                ActionManager.MoveZombie(maze, zombie, player);
                //move_manager.MovePlayer(ref player);
            } while (player.IsAlive && !player.IsEscaped);
        }
    }
}
