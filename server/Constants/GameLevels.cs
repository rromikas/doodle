using System;
namespace GameServer.Constants
{
    public static class LevelValues
    {
        public const int DefaultRedReward = 2;
        public const int DefaultRedDamage = 20;
        public const int DefaultGreenReward = 3;
        public const int DefaultGreenDamage = 15;
        public const int DefaultBlueReward = 5;
        public const int DefaultBlueDamage = 5;
    }
    public enum GameLevels: int
    {
        Easy,
        Medium,
        Hard,
        RandomEasy,
        RandomMedium,
        RandomHard
    }
}
