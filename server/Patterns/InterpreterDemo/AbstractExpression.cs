using GameServer.Models;
using GameServer.Patterns.Command;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.InterpreterDemo
{
    public abstract class AbstractExpression
    {
        protected IGameController _gameController;

        protected IHubCallerClients _clients;

        protected Map _map;

        protected string _expression;

        protected string _playerId;

        public AbstractExpression(IGameController gameController, Map map, IHubCallerClients clients, string expression, string playerId)
        {
            _gameController = gameController;
            _map = map;
            _clients = clients;
            _expression = expression;
            _playerId = playerId;
        }

        public abstract void Interpret();
    }
}
