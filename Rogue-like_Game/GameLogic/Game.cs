﻿using Rogue_like_Game.Entities;
using Rogue_like_Game.Entities.Enemies;
using Rogue_like_Game.Entities.Players;
using Rogue_like_Game.MazeLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.GameLogic
{
    internal class Game
    {
        private Maze maze;
        private Player player;
        private Zombie zombie;
        private Archer archer;

        public Game()
        {
            maze = new Maze(17, 17);
            player = new Player(1, 1, 'P');
            zombie = new Zombie(1, maze.Width - 2, 'Z');
            archer = new Archer(maze.Height - 2, maze.Width - 2, 'A');
        }
        public void Run()
        {
            var acting_game_entities = new List<Entity>() { player, zombie, archer };
            do
            {
                MazeManager.CreateMaze(maze, zombie, archer); //Создаем и инициализируем лабиринт
                GameUpdater.UpdateLoop(maze, player, zombie, archer); //Обновляем игру, пока игрок не найдет выход или умрет

                Console.WriteLine("Do you want to play again? (y/n)");
            } while (Console.ReadKey(true).Key == ConsoleKey.Y);
            //В случае, когда уровень закончится (Игрок умрет или найдет выход),
            //можно будет нажать Y для перехода на следующий уровень с новым лабиринтом,
            //или другую клавишу, чтоб окончательно выйти из игры
            //Выбор за игроком
        }
    }
}
