using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Patterns.Visitor;
using GameServer.Models;
namespace GameServer.Patterns.Visitor
{
    public interface IVisitor
    {
        public void ChangeDamage(BaseObstacle Obstacle);
        public void ChangeReward(BaseFood Food);

    }
}
