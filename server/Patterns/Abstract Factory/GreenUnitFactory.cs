using System;
using System.Collections.Generic;
using GameServer.Models;

namespace GameServer.Patterns
{
    public class GreenUnitFactory : IUnitAbstractFactory
    {
        public BaseFood CreateFood()
        {
            return new GreenFood(Coordinate.GenerateRandom());
        }

        public List<BaseFood> BuildManyFoods(int Quantity)
        {
            List<BaseFood> Foods = new List<BaseFood>();
            for (int i = 0; i < Quantity; i++)
            {
                Foods.Add(CreateFood());
            }

            return Foods;
        }

        public BaseObstacle CreateRock()
        {
            return new GreenRock(Coordinate.GenerateRandom());
        }

        public List<BaseObstacle> BuildManyRocks(int Quantity)
        {
            List<BaseObstacle> Rocks = new List<BaseObstacle>();
            for (int i = 0; i < Quantity; i++)
            {
                Rocks.Add(CreateRock());
            }

            return Rocks;
        }
    }
}
