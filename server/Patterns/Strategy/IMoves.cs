using System;
using GameServer.Models;

namespace GameServer.Patterns.Strategy
{
    public interface IMoves
    {
        Coordinate Move(Coordinate currentPosition);
    }
}
