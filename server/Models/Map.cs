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
        public List<BaseUnit> _blueFoods { get; set; }
        public List<BaseUnit> _blueRocks { get; set; }

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
