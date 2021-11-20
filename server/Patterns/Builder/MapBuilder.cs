using GameServer.Constants;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.Patterns.Builder
{
    public class MapBuilder
    {
        public Map MapObject { get; private set; }

        private readonly IUnitAbstractFactory _mapUnitFactory;


        public MapBuilder(GameLevels level)
        {
            _mapUnitFactory = new LevelFactory().CreateAbstractUnitFactory(level);
        }

        public MapBuilder CreateNew()
        {
            MapObject = new Map();
            return this;
        }

        public MapBuilder AddIslands(int quantity)
        {
            MapObject._islands = IslandBuilder.BuildMany(quantity);
            return this;
        }

        public MapBuilder AddSnowBalls(int quantity)
        {
            MapObject._snowBalls = SnowBallBuilder.BuildMany(quantity);
            return this;
        }

        public MapBuilder AddFoods(int quantity)
        {
            MapObject._foods = new List<BaseFood>();
            for (int i = 0; i < quantity; i++)
                MapObject._foods.Add(_mapUnitFactory.CreateFood());
            return this;
        }

        public MapBuilder AddRocks(int quantity)
        {
            MapObject._rocks = new List<BaseObstacle>();
            for (int i = 0; i < quantity; i++)
                MapObject._rocks.Add(_mapUnitFactory.CreateRock());
            return this;
        }
        public MapBuilder AddBoxes(int quantity)
        {
            MapObject._boxes = new List<Box>();
            for (int i = 0; i < quantity; i++)
                MapObject._boxes = BoxBuilder.BuildMany(quantity, MapObject._foods.Cast<BaseUnit>().ToList());
            return this;
        }


    }
}
