using GameServer.Constants;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private readonly Timer _gameClock;

        public GameHub()
        {
            _gameClock = new Timer(GameTickCallBack, null, 1000, 250);
        }

        public async Task Move(string playerId, Coordinate cooradinate)
        {
            _players[playerId].Coordinate = cooradinate;
            await Clients.All.SendAsync(HubMethods.ALL_PLAYERS_INFO, _players);
        }

        public async Task Login(string playerId)
        {
            _players.Add(playerId, new Player(playerId));
            await Clients.All.SendAsync(HubMethods.ALL_PLAYERS_INFO, _players);
        }

        private async void GameTickCallBack(object sate)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Just a message");
        }
    }
}