using System;
using GameServer.Constants;
using GameServer.Patterns.Strategy;
using GameServer.Patterns.Visitor;
namespace GameServer.Models
{
    public class BlueRock: BaseObstacle, IChangePoints
    {
        static int DAMAGE_POINTS = LevelValues.DefaultBlueDamage;
        public BlueRock(Coordinate coordinate): base (coordinate, DAMAGE_POINTS, ColorTypes.Blue)
        {
            MoveStrategy = new MovesDown();
        }

        public void ChangePoints(IVisitor v)
        {
            v.ChangeDamage(this);
        }
    }
}
