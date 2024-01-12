using Rogue_like_Game.Entities;
using Rogue_like_Game.Entities.Enemies;
using Rogue_like_Game.Entities.Players;
using Rogue_like_Game.MazeLogic;
using Rogue_like_Game.MazeLogic.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_like_Game.GameLogic
{
    internal static class GameUpdater
    {
        public static void UpdateLoop(Maze maze, Player player, Zombie zombie, Archer archer) //обновляем состояние игры, пока игрок не дойдет до выхода или не умрет
        {
            var acting_game_entities = new List<Entity>() { player, zombie, archer }; //Собираем все действующие сущности в список и
            var acting_game_entities_dict = new Dictionary<string, Entity>()          //словарь
            {
                { "Player", player },
                { "Zombie", zombie },
                { "Archer", archer }
            };

            do
            {
                Renderer.PrintMaze(maze);

                foreach (var entity in acting_game_entities)
                {
                    entity.Act(maze, acting_game_entities_dict); //Один и тот же метод через foreach вызывается
                }                                                //у всех игровых сущностей
                                                                 //И отрабатывает по-разному
            } while (player.IsAlive && !player.IsEscaped);

            foreach (var entity in acting_game_entities)
            {
                entity.ResetFields(maze);      //У всех сущностей сбрасываем поля к состоянию начала игры
            }
        }
    }
}
