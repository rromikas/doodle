using GameServer.Constants;
using GameServer.Models;
using GameServer.Patterns.Command;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.InterpreterDemo
{
    public class SetLevelExpression : AbstractExpression
    {

        public SetLevelExpression(IGameController gameController, Map map, IHubCallerClients clients,  string expression, string playerId) : base(gameController, map, clients, expression, playerId) {}

        public override void Interpret()
        {
            try
            {
                int level = int.Parse(_expression.Split("-")[1]);
                _gameController.Run(new SetLevelCommand((GameLevels)level, _map, _clients), _playerId);
            }
            catch (Exception)
            {
                Console.WriteLine("Set level expression is wrong");
            }
            
        }
    }
}
