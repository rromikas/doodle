using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.Patterns.Builder
{
    public static class MapBuilder
    {
        private static readonly IslandBuilder _islandBuilder = new IslandBuilder();

        private static readonly SnowBallBuilder _snowBallBuilder = new SnowBallBuilder();

        private static readonly BlueUnitFactory _blueUnitFactory = new BlueUnitFactory();

        private static readonly RedUnitFactory _redUnitFactory = new RedUnitFactory();

        private static readonly GreenUnitFactory _greenUnitFactory = new GreenUnitFactory();

        private const int _defaultObjectsNumber = 3; 

        public static Map Build()
        {
            Map map = new Map();
            map._islands = BuildIslands();
            map._snowBalls = BuildSnowBalls();
            map._foods = BuildFood();
            map._rocks = BuildRocks();

            return map;
        }

        public static List<Island> BuildIslands()
        {
            return _islandBuilder.BuildMany(_defaultObjectsNumber);
        }

        public static List<SnowBall> BuildSnowBalls()
        {
            return _snowBallBuilder.BuildMany(_defaultObjectsNumber);
        }

        public static List<BaseFood> BuildFood()
        {
            return BuildBlueFoods().Concat(BuildRedFoods()).ToList().Concat(BuildGreenFoods()).ToList();
        }
      
        public static List<BaseFood> BuildBlueFoods()
        {
            return _blueUnitFactory.BuildManyFoods(_defaultObjectsNumber);
        }

        public static List<BaseFood> BuildRedFoods()
        {
            return _redUnitFactory.BuildManyFoods(_defaultObjectsNumber);
        }

        public static List<BaseFood> BuildGreenFoods()
        {
            return _greenUnitFactory.BuildManyFoods(_defaultObjectsNumber);
        }

        public static List<BaseObstacle> BuildRocks()
        {
            return BuildBlueRocks().Concat(BuildRedRocks()).ToList().Concat(BuildGreenRocks()).ToList();
        }

        public static List<BaseObstacle> BuildBlueRocks()
        {
            return _blueUnitFactory.BuildManyRocks(_defaultObjectsNumber);
        }

        public static List<BaseObstacle> BuildRedRocks()
        {
            return _redUnitFactory.BuildManyRocks(_defaultObjectsNumber);
        }

        public static List<BaseObstacle> BuildGreenRocks()
        {
            return _greenUnitFactory.BuildManyRocks(_defaultObjectsNumber);
        }
    }
}
