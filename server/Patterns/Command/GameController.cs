using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Command
{
    public class GameController
    {
        public Dictionary<string, List<AbstractCommand>> commands = new Dictionary<string, List<AbstractCommand>>();

        public async Task Run(AbstractCommand command, string playerId)
        {
            if(!commands.ContainsKey(playerId))
            {
                commands.Add(playerId, new List<AbstractCommand>());
            }

            commands[playerId].Add(command);
            await command.Execute();
        }

        public async void Undo(string playerId)
        {
            if(commands[playerId].Count > 0)
            {
                AbstractCommand command = commands[playerId].Last();
                commands[playerId].Remove(command);
                await command.Undo();
            }
            
        }
    }
}
