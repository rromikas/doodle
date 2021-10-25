using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Command
{
    public class LoginCommand : AbstractCommand
    {
        private string _playerId;

        private Coordinate _coordinate;

        public LoginCommand(string playerId, Coordinate coordinate, Map map, IHubCallerClients clients) : base(map, clients)
        {
            _playerId = playerId;
            _coordinate = coordinate;
        }

        public override async Task Execute()
        {
            _map.AddNewPlayer(_playerId, _coordinate);
            await _clients.All.SendAsync(HubMethods.ALL_PLAYERS_INFO, _map);
            FileLogger.logger.Log(String.Format("New player with id: '{0}' joyned! ", _playerId));
        }

        public override async Task Undo()
        {
            _map.RemovePlayer(_playerId);
            await _clients.All.SendAsync(HubMethods.ALL_PLAYERS_INFO, _map);
            FileLogger.logger.Log(String.Format("Player with id: '{0}' removed! ", _playerId));
        }
    }
}
