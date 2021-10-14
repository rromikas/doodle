using System;
using GameServer.Constants;

namespace GameServer.Models
{
    public class BlueFood: BaseFood
    {
        const int REWARD_POINTS = 1;
        public BlueFood(Coordinate coordinate) : base(coordinate, REWARD_POINTS, ColorTypes.Blue)
        {
        }
    }
}
