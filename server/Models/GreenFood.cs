using System;
using GameServer.Constants;

namespace GameServer.Models
{
    public class GreenFood : BaseFood
    {
        const int REWARD_POINTS = 3;
        public GreenFood(Coordinate coordinate) : base(coordinate, REWARD_POINTS, ColorTypes.Green)
        {
        }
    }
}
