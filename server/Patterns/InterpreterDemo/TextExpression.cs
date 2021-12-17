using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.InterpreterDemo
{
    public class TextExpression : AbstractExpression
    {

        public TextExpression(IGameController gameController, Map map, IHubCallerClients clients,  string expression, string playerId) : base(gameController, map, clients, expression, playerId) {}

        public override void Interpret()
        {
            string[] words = _expression.Split(' ');
            foreach (var word in words)
            {
                if(word.StartsWith("@"))
                {
                    new CommandExpression(_gameController, _map, _clients, word, _playerId).Interpret();
                }
            }
        }
    }
}
