using GameServer.Models;

namespace GameServer.Patterns.Chain
{
    public class PlayerUpdateResolver
    {
        PlayerSpeedResolver playerSpeedResolver;

        PlayerSpeedFromStateResolver playerSpeedFromStateResolver;

        PlayerSpeedFromObstaclesResolver playerSpeedFromObstaclesResolver;

        public PlayerUpdateResolver()
        {
            playerSpeedResolver = new PlayerSpeedResolver();
            playerSpeedFromStateResolver = new PlayerSpeedFromStateResolver();
            playerSpeedFromObstaclesResolver = new PlayerSpeedFromObstaclesResolver();

            playerSpeedResolver.SetNext(playerSpeedFromStateResolver).SetNext(playerSpeedFromObstaclesResolver);
        }

        public void Resolve(Map map)
        {
            playerSpeedResolver.Resolve(map);
        }
    }
}
