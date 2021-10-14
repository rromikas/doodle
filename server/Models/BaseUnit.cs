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
        public IMoves MoveStrategy { get; set; }
        public ColorTypes Color { get; set; }
        public Coordinate Coordinate { get; set; }
        public BaseUnit(Coordinate coordinate, ColorTypes color = ColorTypes.Black)
        {
            MoveStrategy = new MovesNot();

            Coordinate = coordinate;
            Color = color;
        }

        public void TryToMove()
        {
            Coordinate = MoveStrategy.Move(Coordinate);
        }

    }
}
