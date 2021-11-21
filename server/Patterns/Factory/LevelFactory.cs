using System;
using System.Collections.Generic;
using GameServer.Constants;
using GameServer.Models;

namespace GameServer.Patterns
{
    public class LevelFactory: ILevelFactory
    {
        static List<IUnitAbstractFactory> gameLevels = new List<IUnitAbstractFactory>();
        public IUnitAbstractFactory CreateAbstractUnitFactory(GameLevels selectedLevel)
        {
           return GetGameLevel(selectedLevel);
        }

        static IUnitAbstractFactory GetGameLevel(GameLevels selectedLevel)
        {
            if (gameLevels.Count == 0)
            {
                return LevelCreator(selectedLevel);
            }
            else
            {
                foreach (IUnitAbstractFactory f in gameLevels)
                {
                    if (f is GreenUnitFactory && GameLevels.Easy == selectedLevel)
                        return f;
                    else if (f is BlueUnitFactory && GameLevels.Medium == selectedLevel)
                        return f;
                    else if (f is RedUnitFactory && GameLevels.Hard == selectedLevel)
                        return f;
                }
                return LevelCreator(selectedLevel);
            }
        }
        static IUnitAbstractFactory LevelCreator(GameLevels selectedLevel) 
        {
            switch (selectedLevel)
            {
                case GameLevels.Easy:
                    GreenUnitFactory Greenfactory = new GreenUnitFactory();
                    gameLevels.Add(Greenfactory);
                    return Greenfactory;
                case GameLevels.Medium:
                    BlueUnitFactory Bluefactory = new BlueUnitFactory();
                    gameLevels.Add(Bluefactory);
                    return Bluefactory;
                case GameLevels.Hard:
                    RedUnitFactory Redfactory = new RedUnitFactory();
                    gameLevels.Add(Redfactory);
                    return Redfactory;
                default:
                    throw new Exception("Bad selected level");
            }
        }
    }
}
