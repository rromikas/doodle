using System;
using GameServer.Constants;
using GameServer.Patterns.Visitor;

namespace GameServer.Models
{
    public class GreenFood : BaseFood, IChangePoints
    {
        static int REWARD_POINTS = LevelValues.DefaultGreenReward;
        public GreenFood(Coordinate coordinate) : base(coordinate, REWARD_POINTS, ColorTypes.Green)
        {
        }
        public void ChangePoints(IVisitor v)
        {
            v.ChangeReward(this);
        }
    }
}
