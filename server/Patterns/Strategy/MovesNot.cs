using System;
using GameServer.Models;

namespace GameServer.Patterns.Strategy
{
    public class MovesNot: IMoves
    {
        public Coordinate Move(Coordinate currentPossition)
        {
            return currentPossition;
        }
    }
}
