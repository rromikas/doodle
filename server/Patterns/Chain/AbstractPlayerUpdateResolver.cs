

using GameServer.Models;
/**
* @(#) GameResolver.cs
*/
namespace GameServer.Patterns.Chain
{
	public abstract class AbstractPlayerUpdateResolver : IPlayerUpdateResolver
	{
        public IPlayerUpdateResolver _nextResolver;

        public IPlayerUpdateResolver SetNext(IPlayerUpdateResolver handler)
        {
            _nextResolver = handler;
            return handler;
        }

        public abstract void Resolve(Map map);
    }
	
}
