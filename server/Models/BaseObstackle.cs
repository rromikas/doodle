using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;

namespace GameServer.Models
{
    public class BaseObstacle: BaseUnit
    {
        private int _damagePoints;
        public BaseObstacle(Coordinate coordinate, int damagePoints, ColorTypes color = ColorTypes.Black) : base(coordinate, color)
        {
            _damagePoints = damagePoints;
        }
    }
}
