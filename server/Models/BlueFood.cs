using System;
using GameServer.Constants;
using GameServer.Patterns.Visitor;

namespace GameServer.Models
{
    public class BlueFood: BaseFood, IChangePoints
    {
        static int REWARD_POINTS = LevelValues.DefaultBlueReward;
        public BlueFood(Coordinate coordinate) : base(coordinate, REWARD_POINTS, ColorTypes.Blue)
        {
        }

        public void ChangePoints(IVisitor v)
        {
            v.ChangeReward(this);
        }
    }
}
