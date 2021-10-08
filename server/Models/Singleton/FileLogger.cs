using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Singleton
{
    public sealed class FileLogger
    {
        public static FileLogger logger => SingletonHolder._instance;

        readonly object lockObj = new object();
        string filePath = Path.GetFullPath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"./server/Logs/log.log"));


        public void Log(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    streamWriter.WriteLine(String.Format("[{0}] - {1}", DateTime.Now.ToString(), message));
                    streamWriter.Close();
                }
            }
        }

        private class SingletonHolder
        {
            static SingletonHolder() {
               Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"./server/Logs")));
            }

            internal static readonly FileLogger _instance = new FileLogger();
        }
    }
}
