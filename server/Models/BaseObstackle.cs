using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;

namespace GameServer.Models
{
    public class BaseObstacle: BaseUnit
    {
        public int _damagePoints { get; set; }

        public BaseObstacle(Coordinate coordinate, int damagePoints, ColorTypes color = ColorTypes.Black) : base(coordinate, color)
        {
            _damagePoints = damagePoints;
            Size = new Size(30, 30);
        }
    }
}
