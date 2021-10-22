using GameServer.Patterns.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Constants;

namespace GameServer.Models
{
    public class SnowBall: BaseObstacle
    {
        const int DAMAGE_POINTS = 50;

        public SnowBall(Coordinate coordinate): base (coordinate, DAMAGE_POINTS, ColorTypes.Snow)
        {
            MoveStrategy = new MovesDown();
            Size = new Size(50, 50);
        }
    }
}
