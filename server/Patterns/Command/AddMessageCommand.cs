using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using GameServer.Patterns.State;
using GameServer.Patterns.Chain;

namespace GameServer.Patterns.Command
{
    public class AddMessageCommand : AbstractCommand
    {
        private string _playerId;

        private string _text;

        private string _messageid;

        public AddMessageCommand(string playerId, string text, Map map, IHubCallerClients clients) : base(map, clients)
        {
            _playerId = playerId;
            _text = text;
            _map = map;
        }

        public override async Task Execute()
        {
            Message msg = new Message(_playerId, _text);
            _map.AddMessage(msg);
            _messageid = msg.id;
            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            FileLogger.logger.Log(String.Format("Message added. It says: '{0}'", _text));
        }

        public override async Task Undo()
        {
            _map.RemoveMessage(_messageid);
            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            FileLogger.logger.Log(String.Format("Message with id '{0}' removed", _messageid));
        }
    }
}
