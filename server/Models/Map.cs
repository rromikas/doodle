using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Patterns.Strategy;
using System.Text.Json;
using GameServer.Memento;
using GameServer.Constants;
using GameServer.Patterns.Iterator;

namespace GameServer.Models
{
    public class Map
    {
        public GameLevels GameLevel { get; set; }
        public Dictionary<string, Player> _players { get; set; } = new Dictionary<string, Player>();
        public List<SnowBall> _snowBalls { get; set; }
        public List<Island> _islands { get; set; }
        public List<BaseFood> _foods { get; set; }
        public List<BaseObstacle> _rocks { get; set; }

        public List<Box> _boxes { get; set; }

        private Timer _gameClock;


        public Coordinate GetPlayerCoordinateById(string playerId)
        {
            _gameClock = new Timer(GameTickCallBack, null, 1000, 5000);
            return _players[playerId].Coordinate;
        }

        private void GameTickCallBack(object sate)
        {
            MoveAllObstacles();
        }

        private void MoveAllObstacles()
        {
            var obstacles = new CustomList<BaseObstacle>(_rocks);
            var iterator = obstacles.CreateIterator();
            while (iterator.HasNext())
            {
                iterator.Next().TryToMove();
            }
            // foreach (BaseObstacle obstacle in _rocks)
            //     obstacle.TryToMove();
        }

        public void UpdatePlayerById(string playerId, Coordinate coordinate)
        {
            _players[playerId].Coordinate = coordinate;
        }

        public void AddNewPlayer(string playerId, Coordinate coordinate)
        {
            _players.Add(playerId, new Player(playerId));
            _players[playerId].Coordinate = coordinate;
        }

        public void RemovePlayer(string playerId)
        {
            _players.Remove(playerId);
        }

        public BaseFood RemoveFood(string foodId)
        {
            var food = _foods.Find(x => x.Id.CompareTo(foodId) == 0);
            if (food != null)
            {
                _foods.Remove(food);
                return food;
            }
           
            return new BlueFood(new Coordinate());
        }

        public Box RemoveBox(string boxId)
        {
            var box = _boxes.Find(x => x.Id.CompareTo(boxId) == 0);
            if (box != null)
            {
                _boxes.Remove(box);
                return box;
            }

            return new Box(new List<BaseUnit>());
        }

        public void AddBox(Box box)
        {
            _boxes.Add(box);
        }

        public void AddFood(BaseFood food)
        {
            _foods.Add(food);
        }

        public MapSnapshot createSnapshot()
        {
            return new MapSnapshot(this, new List<SnowBall>(_snowBalls), new List<Island>(_islands), new List<BaseFood>(_foods), new List<BaseObstacle>(_rocks), new List<Box>(_boxes), GameLevel);
        }
    }
}
