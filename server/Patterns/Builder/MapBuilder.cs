using GameServer.Constants;
using GameServer.Models;
using GameServer.Patterns.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.Patterns.Builder
{
    public class MapBuilder
    {
        public Map MapObject { get; private set; }

        private readonly IUnitAbstractFactory _mapUnitFactory;

        int DamageFrom;
        int DamageTo;
        int RewardFrom;
        int RewardTo;
        GameLevels GameLevel;

        public MapBuilder(GameLevels level, int mDamageFrom = 0, int mDamageTo = 0, int mRewardFrom = 0, int mRewardTo = 0)
        {
            _mapUnitFactory = new LevelFactory().CreateAbstractUnitFactory(level);
            GameLevel = level;

            if (GameLevels.RandomEasy == level || GameLevels.RandomMedium == level || GameLevels.RandomHard == level)
            {
                DamageFrom = mDamageFrom;
                DamageTo = mDamageTo;
                RewardFrom = mRewardFrom;
                RewardTo = mRewardTo;
            }    
        }

        public MapBuilder CreateNew()
        {
            MapObject = new Map();
            MapObject.GameLevel = GameLevel;
            return this;
        }

        public MapBuilder UpdateExisting(Map mapWithPlayers)
        {
            MapObject = mapWithPlayers;
            MapObject.GameLevel = GameLevel;
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

            if (GameLevels.RandomEasy == GameLevel || GameLevels.RandomMedium == GameLevel || GameLevels.RandomHard == GameLevel)
            {
                PointsChanger pointsChanger = new PointsChanger(DamageFrom, DamageTo, RewardFrom, RewardTo);
                foreach (BaseFood food in MapObject._foods)
                {
                    pointsChanger.ChangeReward(food);
                }
            }
            return this;
        }

        public MapBuilder AddRocks(int quantity)
        {
            MapObject._rocks = new List<BaseObstacle>();
            for (int i = 0; i < quantity; i++)
                MapObject._rocks.Add(_mapUnitFactory.CreateRock());
            if (GameLevels.RandomEasy == GameLevel || GameLevels.RandomMedium == GameLevel || GameLevels.RandomHard == GameLevel)
            {
                PointsChanger pointsChanger = new PointsChanger(DamageFrom, DamageTo, RewardFrom, RewardTo);
                foreach (BaseObstacle obstacle in MapObject._rocks)
                {
                    pointsChanger.ChangeDamage(obstacle);
                }
            }
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
