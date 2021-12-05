using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Visitor
{
    public interface IChangePoints
    {
        public void ChangePoints(IVisitor v);
    }
}
