using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Builder
{
    public static class SnowBallBuilder
    {
        public static SnowBall BuildSingle()
        {
            return new SnowBall(Coordinate.GenerateRandom());
        }      

        public static List<SnowBall> BuildMany(int Quantity)
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
