using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace GameServer.Patterns.Command
{
    public class OpenBoxCommand : AbstractCommand
    {
        private string _playerId;

        private string _boxId;

        private Box box = null;

        public OpenBoxCommand(string playerId, string boxId, Map map, IHubCallerClients clients) : base(map, clients)
        {
            _playerId = playerId;
            _boxId = boxId;
        }

        public override async Task Execute()
        {
            box = _map.RemoveBox(_boxId);
            _map._players[_playerId].AddItem(box);
            box.Id = _boxId;


            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            FileLogger.logger.Log(String.Format("Box with id '{0}' was opened! ", _boxId));
        }

        public override async Task Undo()
        {
            _map._players[_playerId].RemoveItem(box.Id);
            _map.AddBox(box);
            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            FileLogger.logger.Log(String.Format("Box with id '{0}' was closed! ", _boxId));
        }
    }
}
