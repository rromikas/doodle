using System;
using GameServer.Constants;

namespace GameServer.Models
{
    public class RedFood : BaseFood
    {
        const int REWARD_POINTS = 2;
        public RedFood(Coordinate coordinate) : base(coordinate, REWARD_POINTS, ColorTypes.Red)
        {
        }
    }
}
