using System.Threading.Tasks;
using GameServer.Patterns.Command;

namespace GameServer.Models
{
    public interface IGameController
    {
        public Task Run(AbstractCommand command, string playerId);
        public void Undo(string playerId);

    }
}