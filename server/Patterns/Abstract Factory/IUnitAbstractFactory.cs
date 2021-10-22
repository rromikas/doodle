using System;
using GameServer.Models;

namespace GameServer.Patterns
{
    public interface IUnitAbstractFactory
    {
        BaseFood CreateFood();
        BaseObstacle CreateRock();
    }
}
