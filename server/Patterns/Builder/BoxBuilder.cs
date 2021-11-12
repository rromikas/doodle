using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Builder
{
    public static class BoxBuilder
    {
        public static Box BuildSingle(List<BaseUnit> items)
        {
            return new Box(items.Take(new Random().Next(items.Count)).ToList());
        }      

        public static List<Box> BuildMany(int Quantity, List<BaseUnit> items)
        {
            List<Box> boxes = new List<Box>();
            for (int i = 0; i < Quantity; i++)
            { 
                boxes.Add(new Box(items.Take(new Random().Next(items.Count)).ToList()));
            }

            return boxes;
        }
    }
}
