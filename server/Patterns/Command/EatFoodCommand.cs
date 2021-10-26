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
    public class EatFoodCommand : AbstractCommand
    {
        private string _playerId;

        private string _foodId;

        private BaseFood food = null;

        public EatFoodCommand(string playerId, string foodId, Map map, IHubCallerClients clients) : base(map, clients)
        {
            _playerId = playerId;
            _foodId = foodId;
        }

        public override async Task Execute()
        {
            food = _map.RemoveFood(_foodId);
            food.Id = _foodId;
            await _clients.All.SendAsync(HubMethods.REMOVE_UNIT, _foodId);
            FileLogger.logger.Log(String.Format("Food with id '{0}' was eaten! ", _foodId));
        }

        public override async Task Undo()
        {
            _map.AddFood(food);
            await _clients.All.SendAsync(HubMethods.ADD_UNIT, food, _playerId);
            FileLogger.logger.Log(String.Format("Food with id '{0}' was added! ", _foodId));
        }
    }
}
