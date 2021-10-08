using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private static readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
       

        public GameHub() 
        {
            FileLogger.logger.Log("GameHub started!");
        }

        public async Task Move(string playerId, Coordinate cooradinate)
        {
            _players[playerId].Coordinate = cooradinate;
            await Clients.All.SendAsync(HubMethods.PLAYER_MOVE_INFO, playerId, cooradinate);
        }

        public async Task Login(string playerId, Coordinate coordinate)
        {
            _players.Add(playerId, new Player(playerId));
            _players[playerId].Coordinate = coordinate;
            await Clients.All.SendAsync(HubMethods.ALL_PLAYERS_INFO, _players);
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