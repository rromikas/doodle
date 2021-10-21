using System;
using GameServer.Models;

namespace GameServer.Patterns
{
    public class RedUnitFactory : IUnitAbstractFactory
    {
        public BaseFood CreateFood()
        {
            return new RedFood(Coordinate.GenerateRandom());
        }

        public BaseUnit CreateRock()
        {

            return new RedRock(Coordinate.GenerateRandom());
        }
    }
}
