using GameServer.Models;
using GameServer.Patterns.Command;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.InterpreterDemo
{
    public class PauseExpression : AbstractExpression
    {

        public PauseExpression(IGameController gameController, Map map, IHubCallerClients clients,  string expression, string playerId) : base(gameController, map, clients, expression, playerId) {}

        public override void Interpret()
        {
            _gameController.Run(new PauseCommand(_playerId, _map, _clients), _playerId);
        }
    }
}
