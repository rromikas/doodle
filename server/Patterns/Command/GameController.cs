using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Command
{
    public class GameController
    {
        public List<AbstractCommand> commands = new List<AbstractCommand>();

        public async Task Run(AbstractCommand command)
        {
            commands.Add(command);
            await command.Execute();
        }

        public void Undo()
        {
            AbstractCommand command = commands.Last();
            commands.Remove(command);
            command.Undo();
        }
    }
}
