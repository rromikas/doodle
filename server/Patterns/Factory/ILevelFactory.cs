using System;
using GameServer.Constants;
using GameServer.Models;

namespace GameServer.Patterns
{
    public interface ILevelFactory
    {
        IUnitAbstractFactory CreateAbstractUnitFactory(GameLevels selectedLevel);
    }
}
