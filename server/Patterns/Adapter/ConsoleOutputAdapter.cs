using System;
namespace GameServer.Patterns.Adapter
{
    public class ConsoleOutputAdapter: IOutputOperations
    {
        public ConsoleOutputAdapter()
        {
        }

        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}
