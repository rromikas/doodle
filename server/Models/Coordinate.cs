using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Coordinate GenerateRandom()
        {
            return new Coordinate() { X = 5, Y = 5 };
        }
    }
}
