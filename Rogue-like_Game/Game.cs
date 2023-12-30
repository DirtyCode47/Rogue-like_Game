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
        public Game() 
        {
            maze = new Maze(23,23); //в конструктор передаем размер лабиринта. Обязательно нечетное число!!!
            player = new Player(1,1);
        }
        public void Run()
        {
            do
            {
                MazeManager.InitializeMaze(maze);
                MazeManager.GenerateMaze(maze, 1, 1);
                Update();
                player.ResetPlayerFields();
                Console.WriteLine("Do you want to play again? (y/n)");
            } while (Console.ReadKey(true).Key == ConsoleKey.Y);
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
                MoveManager.MovePlayer(maze, player);
                //move_manager.MovePlayer(ref player);
            } while (player.IsAlive && !player.IsEscaped);
        }
    }
}
