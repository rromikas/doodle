﻿using System;
using System.Collections.Generic;
using GameServer.Models;

namespace GameServer.Patterns
{
    public class BlueUnitFactory : IUnitAbstractFactory
    {
        public BaseUnit CreateFood()
        {
            return new BlueFood(Coordinate.GenerateRandom());
        }

        public List<BaseUnit> BuildManyFoods(int Quantity)
        {
            List<BaseUnit> Foods = new List<BaseUnit>();
            for (int i = 0; i < Quantity; i++)
            {
                Foods.Add(CreateFood());
            }

            return Foods;
        }

        public BaseUnit CreateRock()
        {
            return new BlueRock(Coordinate.GenerateRandom());
        }

        public List<BaseUnit> BuildManyRocks(int Quantity)
        {
            List<BaseUnit> Rocks = new List<BaseUnit>();
            for (int i = 0; i < Quantity; i++)
            {
                Rocks.Add(CreateFood());
            }

            return Rocks;
        }
    }
}
