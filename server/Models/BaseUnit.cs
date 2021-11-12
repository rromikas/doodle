using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Constants;
using GameServer.Patterns.Strategy;

namespace GameServer.Models
{
    public class BaseUnit
    {
        public ColorTypes Color { get; set; }
        public Coordinate Coordinate { get; set; }
        public Size Size { get; set; }
        public string Id { get; set; }

        public int Impact { get; set; }

        public List<BaseUnit> Items { get; set; }

        public BaseUnit(Coordinate coordinate, ColorTypes color)
        {

            Coordinate = coordinate;
            Color = color;
            Id = Guid.NewGuid().ToString();
        }

    }
}
