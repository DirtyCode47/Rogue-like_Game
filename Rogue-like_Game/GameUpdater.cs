﻿using MazeRogueLike.Entities;
using Rogue_like_Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MazeRogueLike
{
    internal static class GameUpdater
    {
        public static void Update(Maze maze,Player player,Zombie zombie,Archer archer) //обновляем состояние игры, пока игрок не дойдет до выхода или не умрет
        {
            var acting_game_entities = new List<Entity>() { player, zombie, archer };

            do
            {
                Renderer.PrintMaze(maze);
                ApplyActionToEntities(acting_game_entities, maze, (entity,maze) => entity.Act(maze));

            } while (player.IsAlive && !player.IsEscaped);

            ApplyActionToEntities(acting_game_entities, maze, (entity, maze) => entity.ResetFields(maze));
        }

        private static void ApplyActionToEntities(List<Entity> acting_game_entities, Maze maze, Action<Entity, Maze> entity_action)
        {
            foreach (var entity in acting_game_entities)
            {
                entity_action(entity, maze);   //Решил сделать так, потому что два foreach для сущностей были почти идентичны
            }                                  //Отличалась только вызываемая функция
        }
        //public static bool CheckIfPlayerShouldDie() { return true; }
    }
}