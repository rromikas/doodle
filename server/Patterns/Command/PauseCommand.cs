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
    public class PauseCommand : AbstractCommand
    {
        private string _playerId;

        public PauseCommand(string playerId, Map map, IHubCallerClients clients) : base(map, clients)
        {
            _playerId = playerId;
        }

        public override async Task Execute()
        {
            await _clients.All.SendAsync(HubMethods.PAUSE, _playerId);
        }

        public override async Task Undo()
        {
            await _clients.All.SendAsync(HubMethods.RESUME, _playerId);
        }
    }
}
