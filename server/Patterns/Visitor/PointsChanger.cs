using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Visitor
{
    public class PointsChanger : IVisitor {

        Random rand = new Random();
        int DamageFrom;
        int DamageTo; 
        int RewardFrom;
        int RewardTo;

        public PointsChanger(int mDamageFrom, int mDamageTo, int mRewardFrom, int mRewardTo)
        {
            DamageFrom = mDamageFrom;
            DamageTo = mDamageTo;
            RewardFrom = mRewardFrom;
            RewardTo = mRewardTo;
        }
        public void ChangeDamage(BaseObstacle Obstacle)
        {
            Obstacle.Impact -= rand.Next(DamageFrom, DamageTo); 
        }

        public void ChangeReward(BaseFood Food)
        {
            Food.Impact += rand.Next(RewardFrom, RewardTo);
        }
    }
}
