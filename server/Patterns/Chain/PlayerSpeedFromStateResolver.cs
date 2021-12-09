

using GameServer.Models;
using System.Collections.Generic;
/**
* @(#) FoodResolver.cs
*/
namespace GameServer.Patterns.Chain
{
	public class PlayerSpeedFromStateResolver : AbstractPlayerUpdateResolver
	{
		public override void Resolve(Map map)
        {
			foreach (KeyValuePair<string, Player> pair in map._players)
			{
				pair.Value.UpdateSpeedByState();
			}

			_nextResolver.Resolve(map);
		}
	}
	
}
