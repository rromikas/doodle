using System;
using System.Collections.Generic;

namespace GameServer.Patterns.State
{
    public class MysteryBoxGenerator
    {
        public MysteryBoxGenerator()
        {

        }

        public PlayerState GenerateBox(PlayerState previousState)
        {
            var random = new Random();
            var list = new List<PlayerState> {new BoostedState(previousState), new SlowedDownState(previousState), };

            return list[random.Next(list.Count)];
        }
    }
}
