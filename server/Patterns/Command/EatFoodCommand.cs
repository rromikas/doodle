using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using GameServer.Patterns.State;
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

        private PlayerStateSpeedResolver playerStateSpeedResolver;

        public EatFoodCommand(string playerId, string foodId, Map map, IHubCallerClients clients) : base(map, clients)
        {
            _playerId = playerId;
            _foodId = foodId;
            playerStateSpeedResolver = new PlayerStateSpeedResolver();
        }

        public override async Task Execute()
        {
            food = _map.RemoveFood(_foodId);
            _map._players[_playerId].AddItem(food);
            food.Id = _foodId;

            playerStateSpeedResolver.Resolve(_map);
            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            FileLogger.logger.Log(String.Format("Food with id '{0}' was eaten! ", _foodId));
        }

        public override async Task Undo()
        {
            _map._players[_playerId].RemoveItem(food.Id);
            _map.AddFood(food);
            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            FileLogger.logger.Log(String.Format("Food with id '{0}' was added! ", _foodId));
        }
    }
}
