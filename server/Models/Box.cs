using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Box : Composite
    {
        public Box(List<BaseUnit> items): base(Coordinate.GenerateRandom(), Constants.ColorTypes.Brown)
        {
            Items = items;
            Size = new Size(50, 50);
        }
    }
}
