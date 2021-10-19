using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using GameServer.Patterns.Builder;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly Map _map = null;

        public GameHub() 
        {
            FileLogger.logger.Log("GameHub started!");
            _map = MapBuilder.Build();
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