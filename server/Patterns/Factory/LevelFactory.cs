using System;
using GameServer.Constants;
using GameServer.Models;

namespace GameServer.Patterns
{
    public class LevelFactory: ILevelFactory
    {
        public IUnitAbstractFactory CreateAbstractUnitFactory(GameLevels selectedLevel)
        {
            switch(selectedLevel)
            {
                case GameLevels.Easy:
                    return new GreenUnitFactory();
                case GameLevels.Medium:
                    return new BlueUnitFactory();
                case GameLevels.Hard:
                    return new RedUnitFactory();
                default:
                    throw new Exception("Bad selected level");
            }
        }
    }
}
