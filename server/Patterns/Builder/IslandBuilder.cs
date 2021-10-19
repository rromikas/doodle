using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Builder
{
    public class IslandBuilder
    {
        public Island BuildSingle()
        {
            return new Island(Coordinate.GenerateRandom());
        }

        public List<Island> BuildMany(int Quantity)
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
