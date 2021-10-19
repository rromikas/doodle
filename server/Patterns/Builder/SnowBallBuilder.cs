using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Builder
{
    public class SnowBallBuilder
    {
        public SnowBall BuildSingle()
        {
            return new SnowBall(Coordinate.GenerateRandom());
        }      

        public List<SnowBall> BuildMany(int Quantity)
        {
            List<SnowBall> SnowBalls = new List<SnowBall>();
            for (int i = 0; i < Quantity; i++)
            {
                SnowBalls.Add(new SnowBall(Coordinate.GenerateRandom()));
            }

            return SnowBalls;
        }
    }
}
