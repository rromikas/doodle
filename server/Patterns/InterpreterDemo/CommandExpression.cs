using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.InterpreterDemo
{
    public class CommandExpression : AbstractExpression
    {

        public CommandExpression(IGameController gameController, Map map, IHubCallerClients clients, string expression, string playerId) : base(gameController, map, clients, expression, playerId) {}

        public override void Interpret()
        {
            if(_expression.StartsWith("@pause"))
            {
                new PauseExpression(_gameController, _map, _clients, _expression, _playerId).Interpret();
            }
            else if (_expression.StartsWith("@setLevel"))
            {
                new SetLevelExpression(_gameController, _map, _clients, _expression, _playerId).Interpret();
            }
            else if(_expression.StartsWith("@undo"))
            {
                new UndoExpression(_gameController, _map, _clients, _expression, _playerId).Interpret();
            }
        }
    }
}
