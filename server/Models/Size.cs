using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Size
    {
        private readonly int SizeX;
        private readonly int SizeY;

        public Size(int sizeX = 25, int sizeY = 25)
        {
            SizeX = sizeX;
            SizeY = sizeY;
        }

        public int GetSizeX()
        {
            return SizeX;
        }

        public int GetSizeY()
        {
            return SizeY;
        }
    }
}
