using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;
using GameServer.Patterns.Visitor;

namespace GameServer.Models
{
    public class RedRock : BaseObstacle, IChangePoints
    {
        static int DAMAGE_POINTS = LevelValues.DefaultRedDamage;
        public RedRock(Coordinate coordinate) : base(coordinate, DAMAGE_POINTS, ColorTypes.Red)
        {
            MoveStrategy = new MovesZigzag();
        }
        public void ChangePoints(IVisitor v)
        {
            v.ChangeDamage(this);
        }
    }
}

