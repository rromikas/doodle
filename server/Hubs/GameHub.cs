using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using GameServer.Patterns.Builder;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace GameServer.Hubs
{

    public class GameHub : Hub
    {
        private static Map _map = null;
        public GameHub() 
        {
            if(_map == null)
            {
                FileLogger.logger.Log("GameHub started!");
                _map = MapBuilder.Build();
            }
            FileLogger.logger.Log(JsonSerializer.Serialize(_map));
        }

        public async Task Move(string playerId, Coordinate coordinate)
        {
            _map.UpdatePlayerById(playerId, coordinate);
            await Clients.All.SendAsync(HubMethods.PLAYER_MOVE_INFO, playerId, coordinate, _map);
        }

        public async Task Login(string playerId, Coordinate coordinate)
        {
            _map.AddNewPlayer(playerId, coordinate);
            await Clients.All.SendAsync(HubMethods.ALL_PLAYERS_INFO, _map);
            FileLogger.logger.Log(String.Format("New player with id: '{0}' joyned! ", playerId));
        }

        public async Task Eat(string playerId, string foodId)
        {
            _map.RemoveFood(foodId);
            await Clients.All.SendAsync(HubMethods.REMOVE_UNIT, foodId);
            FileLogger.logger.Log(String.Format("Food with id '{0}' was eaten! ", foodId));
        }


        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}