using GameServer.Constants;
using GameServer.Models;
using GameServer.Models.Singleton;
using GameServer.Patterns.Builder;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using GameServer.Patterns.Command;
using GameServer.Patterns;
using System.Threading;

namespace GameServer.Hubs
{

    public class GameHub : Hub
    {
        private static ILevelFactory _levelFactory;
        private static Map _map = null;
        private static Boolean paused = false;

        private static GameController _gameController = new GameController();

        public GameHub() 
        {
            _levelFactory = new LevelFactory();
            if(_map == null)
            {
                var rand = new Random();
                GameLevels level = (GameLevels)rand.Next(3);
                MapBuilder mapBuilder = new MapBuilder(_levelFactory.CreateAbstractUnitFactory(level));
                _map = mapBuilder.CreateNew().AddFoods(10).AddIslands(10).AddRocks(10).AddSnowBalls(10).MapObject;

            }

            FileLogger.logger.Log("GameHub started!");

            FileLogger.logger.Log(JsonSerializer.Serialize(_map));
        }
        public async Task UpdateMap()
        {
             await Clients.All.SendAsync(HubMethods.MOVE_OBSTACLES, _map);
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