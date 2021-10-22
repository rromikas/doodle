using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Constants;

namespace GameServer.Models
{
    public class Island : BaseObstacle
    {
        const int DAMAGE_POINTS = 0;
        public Island(Coordinate coordinate) : base(coordinate, DAMAGE_POINTS, ColorTypes.Brown)
        {
            var rand = new Random();
            Size = new Size(rand.Next(40, 250), rand.Next(40, 250));
        }
    }
}
