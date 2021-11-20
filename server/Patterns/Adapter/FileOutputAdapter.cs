using System;
using System.IO;

namespace GameServer.Patterns.Adapter
{
    public class FileOutputAdapter:IOutputOperations
    {
        string filePath = Path.GetFullPath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"./server/Logs/log.log"));


        public FileOutputAdapter()
        {
            Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"./server/Logs")));
        }

        public void Write(string output)
        {
            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(String.Format("[{0}] - {1}", DateTime.Now.ToString(), output));
                }
            }
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(String.Format("[{0}] - {1}", DateTime.Now.ToString(), output));
            }
        }
    }
}
