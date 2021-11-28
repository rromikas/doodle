using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.State
{
    public class PlayerStateSpeedResolver
    {
        public void Resolve(Map map)
        {
            foreach(KeyValuePair<string, Player> pair in map._players)
            {
                pair.Value.UpdateSpeedByState();
            }
        }
    }
}
