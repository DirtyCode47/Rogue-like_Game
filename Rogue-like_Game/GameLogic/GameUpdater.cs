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
            var acting_game_entities = new List<Entity>() { player, zombie, archer };
            var acting_game_entities_dict = new Dictionary<string, Entity>()
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
                    entity.Act(maze, acting_game_entities_dict);
                }

            } while (player.IsAlive && !player.IsEscaped);

            foreach (var entity in acting_game_entities)
            {
                entity.ResetFields(maze);
            }
        }
    }
}
