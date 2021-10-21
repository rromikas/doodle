using System;
using GameServer.Constants;

namespace GameServer.Models
{
    public class BaseFood : BaseUnit, ICloneable
    {
        private int _pointReward;

        public BaseFood(Coordinate coordinate, int pointReward, ColorTypes color) : base(coordinate, color)
        {
            _pointReward = pointReward;
        }

        public object Clone()
        {
            return new BaseFood(this.Coordinate, _pointReward, this.Color);
        }
    }
}