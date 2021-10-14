using System;
using GameServer.Models;

namespace GameServer.Patterns
{
    public interface IUnitAbstractFactory
    {
        BaseUnit CreateFood();
        BaseUnit CreateRock();
    }
}
