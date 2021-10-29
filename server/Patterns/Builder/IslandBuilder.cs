using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Builder
{
    public static class IslandBuilder
    {
        public static Island BuildSingle()
        {
            return new Island(Coordinate.GenerateRandom());
        }

        public static List<Island> BuildMany(int Quantity)
        {
            List<Island> Islands = new List<Island>();
            for (int i = 0; i < Quantity; i++)
            {
                Islands.Add(BuildSingle());
            }

            return Islands;
        }
    }
}
