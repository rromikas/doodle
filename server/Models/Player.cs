using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Player: BaseUnit
    {
        public string UserName { get; set; }
       
        public Player(string userName)
        {
            UserName = userName;
        }
    }
}
