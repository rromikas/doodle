using GameServer.Constants;
using GameServer.Memento;
using GameServer.Models;
using GameServer.Models.Singleton;
using GameServer.Patterns.Builder;
using GameServer.Patterns.Chain;
using GameServer.Patterns.State;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Command
{
    public class SetLevelCommand : AbstractCommand
    {
        private MapSnapshot Snapshot = null;
        private GameLevels Level;

        public SetLevelCommand(GameLevels level, Map map, IHubCallerClients clients) : base(map, clients)
        {
            Snapshot = map.createSnapshot();
            Level = level;
            _map = map;
        }

        public override async Task Execute()
        {
            MapBuilder mapBuilder;
            switch (Level)
            {
                case GameLevels.RandomEasy:
                    mapBuilder = new MapBuilder(Level, 0, 2, 0, 2);
                    break;
                case GameLevels.RandomMedium:
                    mapBuilder = new MapBuilder(Level, -2, 5, -5, 2);
                    break;
                case GameLevels.RandomHard:
                    mapBuilder = new MapBuilder(Level, -5, 10, -10, 5);
                    break;
                default:
                    mapBuilder = new MapBuilder(Level);
                    break;
            }
            _map = mapBuilder.UpdateExisting(_map).AddFoods(10).AddIslands(10).AddRocks(10).AddSnowBalls(10).AddBoxes(10).MapObject;
            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            await _clients.All.SendAsync(HubMethods.SET_LEVEL, Level);
            FileLogger.logger.Log(String.Format("Level was changed into '{0}'", Level));
        }

        public override async Task Undo()
        {
            Snapshot.restore();
            await _clients.All.SendAsync(HubMethods.ALL_INFO, _map);
            FileLogger.logger.Log(String.Format("Level undo! "));
        }
    }
}
