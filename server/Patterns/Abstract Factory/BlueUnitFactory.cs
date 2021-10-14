using System;
using GameServer.Models;

namespace GameServer.Patterns
{
    public class BlueUnitFactory : IUnitAbstractFactory
    {
        public BaseUnit CreateFood()
        {
            return new BlueFood(Coordinate.GenerateRandom());
        }

        public BaseUnit CreateRock()
        {

            return new BlueRock(Coordinate.GenerateRandom());
        }
    }
}
