using System;
using GameServer.Constants;

namespace GameServer.Models
{
    public class BaseFood: BaseUnit
    {

        public BaseFood(Coordinate coordinate, int pointReward, ColorTypes color) : base(coordinate, color)
        {
            Impact = pointReward;
            Size = new Size(20, 20);
        }
    }
}
