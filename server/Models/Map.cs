using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Map
    {
        public Dictionary<string, Player> _players = new Dictionary<string, Player>();
        public List<SnowBall> _snowBalls;
        public List<Island> _islands;
        public List<BaseUnit> _blueFoods;
        public List<BaseUnit> _blueRocks;

        public void UpdatePlayerById(string playerId, Coordinate coordinate)
        {
            _players[playerId].Coordinate = coordinate;
        }

        public void AddNewPlayer(string playerId, Coordinate coordinate)
        {
            _players.Add(playerId, new Player(playerId));
            _players[playerId].Coordinate = coordinate;
        }
    }
}
