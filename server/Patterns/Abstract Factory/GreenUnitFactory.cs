using System;
using GameServer.Models;

namespace GameServer.Patterns
{
    public class GreenUnitFactory : IUnitAbstractFactory
    {
        public BaseUnit CreateFood()
        {
            return new GreenFood(Coordinate.GenerateRandom());
        }

        public BaseUnit CreateRock()
        {

            return new GreenRock(Coordinate.GenerateRandom());
        }
    }
}
