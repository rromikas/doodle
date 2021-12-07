using GameServer.Constants;
using GameServer.Models;
using System;
using System.Collections.Generic;

namespace GameServer.Memento
{
  public class MapSnapshot
  {
        private List<SnowBall> _snowBalls;
        private List<Island> _islands;
        private List<BaseFood> _foods;
        private List<BaseObstacle> _rocks;
        private List<Box> _boxes;
        private GameLevels _gameLevel;

        public Map _map;
        public MapSnapshot(Map map, List<SnowBall> snowBalls, List<Island> islands, List<BaseFood> foods,List<BaseObstacle> rocks, List<Box> boxes, GameLevels gameLevel)
        {
            _map = map;
            _snowBalls = snowBalls;
            _islands = islands;
            _foods = foods;
            _rocks = rocks;
            _boxes = boxes;
            _gameLevel = gameLevel;
        }

        public void restore()
        {
            _map._boxes = _boxes;
            _map._foods = _foods;
            _map._islands = _islands;
            _map._rocks = _rocks;
            _map._snowBalls = _snowBalls;
            _map.GameLevel = _gameLevel;
        }

  }
}
