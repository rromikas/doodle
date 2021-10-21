using System;
using GameServer.Constants;

namespace GameServer.Models
{
    public class BaseFood: BaseUnit
    {
        public int _pointReward { get; set; }

        public BaseFood(Coordinate coordinate, int pointReward, ColorTypes color) : base(coordinate, color)
        {
            _pointReward = pointReward;
            Size = new Size(10, 10);
        }
    }
}
