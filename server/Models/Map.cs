using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameServer.Models
{
    public class Map
    {
        public Dictionary<string, Player> _players { get; set; } = new Dictionary<string, Player>();
        public List<SnowBall> _snowBalls { get; set; }
        public List<Island> _islands { get; set; }
        public List<BaseFood> _foods { get; set; }
        public List<BaseObstacle> _rocks { get; set; }

        public Coordinate GetPlayerCoordinateById(string playerId)
        {
            return _players[playerId].Coordinate;
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

        public void AddFood(BaseFood food)
        {
            _foods.Add(food);
        }
    }
}
