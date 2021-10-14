using System;
using GameServer.Constants;

namespace GameServer.Models
{
    public class BaseFood: BaseUnit
    {
        private int _pointReward;
        public BaseFood(Coordinate coordinate, int pointReward, ColorTypes color) : base(coordinate, color)
        {
            _pointReward = pointReward;
        }
    }
}
