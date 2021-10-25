using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Command
{
    public abstract class AbstractCommand
    {
        protected Map _map;

        protected IHubCallerClients _clients;

        public AbstractCommand(Map map, IHubCallerClients clients)
        {
            _map = map;
            _clients = clients;
        }

        public abstract Task Execute();

        public abstract Task Undo();
    }
}
