using GameServer.Constants;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private static readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private readonly Timer _gameClock;

        public GameHub()
        {}

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
        }

      
    }
}