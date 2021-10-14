using System;

using GameServer.Models;

namespace GameServer.Patterns.Strategy
{
    public class MovesZigzag : IMoves
    {
        public Coordinate Move(Coordinate currentPossition)
        {
            const double WIDE_COF = 2;
            return new Coordinate() { X = (int)(currentPossition.X + Math.Cos(currentPossition.Y / 3.33) * WIDE_COF), Y = currentPossition.Y - 1 };
        }
    }
}
