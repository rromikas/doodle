using System;
using GameServer.Constants;
using GameServer.Patterns.Visitor;

namespace GameServer.Models
{
    public class RedFood : BaseFood, IChangePoints
    {
        static int REWARD_POINTS = LevelValues.DefaultRedReward;
        public RedFood(Coordinate coordinate) : base(coordinate, REWARD_POINTS, ColorTypes.Red)
        {
        }
        public void ChangePoints(IVisitor v)
        {
            v.ChangeReward(this);
        }
    }
}
