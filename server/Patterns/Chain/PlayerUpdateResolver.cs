using GameServer.Models;

namespace GameServer.Patterns.Chain
{
    public class PlayerUpdateResolver
    {
        PlayerSpeedResolver playerSpeedResolver;

        PlayerSpeedFromStateResolver playerSpeedFromStateResolver;

        public PlayerUpdateResolver()
        {
            playerSpeedResolver = new PlayerSpeedResolver();
            playerSpeedFromStateResolver = new PlayerSpeedFromStateResolver();

            playerSpeedResolver.SetNext(playerSpeedFromStateResolver);
        }

        public void Resolve(Map map)
        {
            playerSpeedResolver.Resolve(map);
        }
    }
}
