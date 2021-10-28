using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;

namespace GameServer.Models
{
    public class BaseObstacle: BaseUnit
    {
        public IMoves MoveStrategy { get; set; }

        public BaseObstacle(Coordinate coordinate, int damagePoints, ColorTypes color = ColorTypes.Black) : base(coordinate, color)
        {
            MoveStrategy = new MovesNot();
            Impact = -damagePoints;
            var rand = new Random();
            var size = rand.Next(30, 50);
            Size = new Size(size, size);
        }

        public void TryToMove()
        {
            Coordinate = MoveStrategy.Move(Coordinate);
        }
    }
}
