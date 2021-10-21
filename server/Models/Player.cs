using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Player: BaseUnit
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public int Speed { get; set; } = 10;
       
        public Player(string userName): base(new Coordinate(), Constants.ColorTypes.Black)
        {
            UserName = userName;
        }
    }
}
