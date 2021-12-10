using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Message
    {
        public string user { get; set; }
        public string text { get; set; }
        public string id { get; set; }

        public Message(string u, string t)
        {
            user = u;
            text = t;
            id = Guid.NewGuid().ToString();
        }

        public Message() { }
    }
}
