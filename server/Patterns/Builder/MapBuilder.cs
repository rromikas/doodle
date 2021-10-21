using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Builder
{
    public static class MapBuilder
    {
        private static readonly IslandBuilder _islandBuilder = new IslandBuilder();

        private static readonly SnowBallBuilder _snowBallBuilder = new SnowBallBuilder();

        private static readonly BlueUnitFactory _blueUnitFactory = new BlueUnitFactory();

        public static Map Build()
        {
            Map map = new Map();
            map._islands = BuildIslands();
            map._snowBalls = BuildSnowBalls();
            map._foods = BuildBlueFoods();
            map._rocks = BuildBlueRocks();

            return map;
        }

        public static List<Island> BuildIslands()
        {
            return _islandBuilder.BuildMany(3);
        }

        public static List<SnowBall> BuildSnowBalls()
        {
            return _snowBallBuilder.BuildMany(3);
        }

        public static List<BaseFood> BuildBlueFoods()
        {
            return _blueUnitFactory.BuildManyFoods(3);
        }

        public static List<BaseUnit> BuildBlueRocks()
        {
            return _blueUnitFactory.BuildManyRocks(3);
        }
    }
}
