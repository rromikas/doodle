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
    public class MoveCommand : AbstractCommand
    {
        private string _playerId;

        private Coordinate _coordinate;

        private Coordinate _lastCoordinate;

        public MoveCommand(string playerId, Coordinate coordinate, Map map, IHubCallerClients clients) : base(map, clients)
        {
            _playerId = playerId;
            _coordinate = coordinate;
        }

        public override async Task Execute()
        {
            _lastCoordinate = _map.GetPlayerCoordinateById(_playerId);
            _map.UpdatePlayerById(_playerId, _coordinate);
            await _clients.All.SendAsync(HubMethods.PLAYER_MOVE_INFO, _playerId, _coordinate);
        }

        public override async Task Undo()
        {
            _map.UpdatePlayerById(_playerId, _coordinate);
            await _clients.All.SendAsync(HubMethods.PLAYER_MOVE_INFO, _playerId, _lastCoordinate, true);
        }
    }
}
