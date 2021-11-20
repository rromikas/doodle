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
            var rand = new Random();
            return new Coordinate() { X = rand.Next(0, 900), Y = rand.Next(0, 9000) };
        }
    }
}
