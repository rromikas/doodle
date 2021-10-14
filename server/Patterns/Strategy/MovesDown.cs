using System;
using GameServer.Models;

namespace GameServer.Patterns.Strategy
{
    public class MovesDown: IMoves
    {
        public Coordinate Move(Coordinate currentPossition)
        {
            return new Coordinate() { X = currentPossition.X, Y = currentPossition.Y - 1 } ;
        }
    }
}
