using Patterns.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.State
{
    public class MysteryBoxGenerator
    {
        public static PlayerState GenerateBox(PlayerState previousState)
        {
            var random = new Random();
            var list = new List<PlayerState> {new BoostedState(previousState), new SlowedDownState(previousState), };

            return list[random.Next(list.Count)];
        }
    }
}
