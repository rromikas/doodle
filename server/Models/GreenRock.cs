using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;

namespace GameServer.Models
{
    public class GreenRock : BaseObstacle
    {
        const int DAMAGE_POINTS = 15;
        public GreenRock(Coordinate coordinate) : base(coordinate, DAMAGE_POINTS, ColorTypes.Green)
        {
            MoveStrategy = new MovesZigzag();
        }
    }
}

