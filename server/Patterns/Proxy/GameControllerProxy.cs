using System.Threading.Tasks;
using GameServer.Models.Singleton;
using GameServer.Patterns.Command;

namespace GameServer.Models
{
    public class GameControllerProxy: IGameController
    {
        private readonly GameController _gameController = new GameController();
        
        public async Task Run(AbstractCommand command, string playerId)
        {
            FileLogger.logger.Log($"{playerId} is using command: {command.GetType().Name}");

            await _gameController.Run(command, playerId);
        }

        public void Undo(string playerId)
        {
            FileLogger.logger.Log($"{playerId} undoing his last command");

             _gameController.Undo(playerId);
        }
    }
}