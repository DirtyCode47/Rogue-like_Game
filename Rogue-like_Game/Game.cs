using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game
{
    internal class Game
    {
        private int width = 23;
        private int height = 23;
        private char[,] maze;
        private MazeManager maze_manager;
        private Renderer renderer;
        public Game() 
        {
            maze_manager = new MazeManager(width,height);
            renderer = new Renderer();
        }
        public void Run()
        {
            maze_manager.InitializeMaze(ref maze);
            maze_manager.GenerateMaze(ref maze, 1, 1);
            do
            {

                //PrintMaze();
                //PlacePlayer();
                //PlaceMeleeEnemy();
                //PlaceArcherEnemy();
                //RunGame();
                renderer.PrintMaze(maze, width, height);
                Thread.Sleep(1000);
                Console.WriteLine("Do you want to play again? (y/n)");
            } while (Console.ReadKey(true).Key == ConsoleKey.Y);
        }
    }
}
