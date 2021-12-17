using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.InterpreterDemo
{
    public class UndoExpression : AbstractExpression
    {

        public UndoExpression(IGameController gameController, Map map, IHubCallerClients clients, string expression, string playerId) : base(gameController,  map, clients, expression, playerId) {}

        public override void Interpret()
        {
            _gameController.Undo(_playerId);
        }
    }
}
