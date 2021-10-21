using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Size
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public Size(int sizeX = 25, int sizeY = 25)
        {
            SizeX = sizeX;
            SizeY = sizeY;
        }
    }
}
