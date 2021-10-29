using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Constants
{
    public static class HubMethods
    {
        public const string ALL_PLAYERS_INFO = "PlayersInfo";
        public const string PLAYER_MOVE_INFO = "PlayerMoveInfo";
        public const string REMOVE_UNIT = "RemoveUnit";
        public const string ADD_UNIT = "AddUnit";
        public const string PAUSE = "Pause";
        public const string RESUME = "Resume";
        public const string LOGOUT = "Logout";
        public const string MOVE_OBSTACLES = "MoveObstacles";
    }
}
