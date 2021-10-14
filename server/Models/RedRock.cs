using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;

namespace GameServer.Models
{
    public class RedRock : BaseObstacle
    {
        const int DAMAGE_POINTS = 20;
        public RedRock(Coordinate coordinate) : base(coordinate, DAMAGE_POINTS, ColorTypes.Red)
        {
            MoveStrategy = new MovesZigzag();
        }
    }
}

