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
        private static Map _map = null;
        private static Boolean paused = false;
        private static GameController _gameController = new GameController();
        private static Nullable<GameLevels> level = null;

        public GameHub() 
        {
            FileLogger.logger.Log("GameHub started!");
            FileLogger.logger.Log(JsonSerializer.Serialize(_map));
        }

        public void SetLevel(GameLevels lvl)
        {
            if (_map == null)
            {
                level = lvl;
                MapBuilder mapBuilder;
                switch (level)
                {
                    case GameLevels.RandomEasy:
                        mapBuilder = new MapBuilder(lvl,0,2,0,2);
                        break;
                    case GameLevels.RandomMedium:
                        mapBuilder = new MapBuilder(lvl, -2, 5, -5, 2);
                        break;
                    case GameLevels.RandomHard:
                        mapBuilder = new MapBuilder(lvl, -5, 10, -10, 5);
                        break;
                    default:
                        mapBuilder = new MapBuilder(lvl);
                        break;
                }
                _map = mapBuilder.CreateNew().AddFoods(10).AddIslands(10).AddRocks(10).AddSnowBalls(10).AddBoxes(10).MapObject;
                Clients.All.SendAsync(HubMethods.SET_LEVEL, lvl);
            }
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

        public async Task OpenBox(string playerId, string boxId)
        {
            await _gameController.Run(new OpenBoxCommand(playerId, boxId, _map, Clients), playerId);
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
            await Clients.All.SendAsync("UserConnected", level);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}