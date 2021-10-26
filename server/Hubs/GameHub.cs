using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using GameServer.Patterns.Builder;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using GameServer.Patterns.Command;

namespace GameServer.Hubs
{

    public class GameHub : Hub
    {
        private static Map _map = null;

        private static Boolean paused = false;

        private static GameController _gameController = new GameController();

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
            await _gameController.Run(new MoveCommand(playerId, coordinate, _map, Clients), playerId);
        }

        public async Task Login(string playerId, Coordinate coordinate)
        {
            await _gameController.Run(new LoginCommand(playerId, coordinate, _map, Clients), playerId);
        }

        public async Task Eat(string playerId, string foodId)
        {
            await _gameController.Run(new EatFoodCommand(playerId, foodId, _map, Clients), playerId);
        }

        public async Task Pause(string playerId)
        {
            await _gameController.Run(new PauseCommand(playerId, _map, Clients), playerId);
            paused = !paused;
        }

        public void Undo(string playerId)
        {
             _gameController.Undo(playerId);
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