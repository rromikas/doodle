using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Patterns.Adapter;

namespace GameServer.Models.Singleton
{
    public sealed class FileLogger
    {
        public static FileLogger logger => SingletonHolder._instance;

        readonly object lockObj = new object();
        string filePath = Path.GetFullPath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"./server/Logs/log.log"));

        readonly IOutputOperations output = new FileOutputAdapter();

        public void Log(string message)
        {
            lock (lockObj)
            {
                output.Write(message);
            }
        }

        private class SingletonHolder
        {
            static SingletonHolder() {
            }

            internal static readonly FileLogger _instance = new FileLogger();
        }
    }
}
