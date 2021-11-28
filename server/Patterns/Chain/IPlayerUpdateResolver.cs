using GameServer.Models;

namespace GameServer.Patterns.Chain
{
    public interface IPlayerUpdateResolver
    {
        public IPlayerUpdateResolver SetNext(IPlayerUpdateResolver resolver);

        public void Resolve(Map map);
    }
}
