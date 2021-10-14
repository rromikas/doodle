using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;

namespace GameServer.Models
{
    public class BlueRock: BaseObstacle
    {
        const int DAMAGE_POINTS = 5;
        public BlueRock(Coordinate coordinate): base (coordinate, DAMAGE_POINTS, ColorTypes.Blue)
        {
            MoveStrategy = new MovesDown();
        }
    }
}
