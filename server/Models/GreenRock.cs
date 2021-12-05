using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;
using GameServer.Patterns.Visitor;

namespace GameServer.Models
{
    public class GreenRock : BaseObstacle, IChangePoints
    {
        static int DAMAGE_POINTS = LevelValues.DefaultGreenDamage;
        public GreenRock(Coordinate coordinate) : base(coordinate, DAMAGE_POINTS, ColorTypes.Green)
        {
            MoveStrategy = new MovesZigzag();
        }
        public void ChangePoints(IVisitor v)
        {
            v.ChangeDamage(this);
        }
    }
}

