

using GameServer.Models;
using System.Collections.Generic;
/**
* @(#) FoodResolver.cs
*/
namespace GameServer.Patterns.Chain
{
	public class PlayerSpeedResolver : AbstractPlayerUpdateResolver
	{
		public override void Resolve(Map map)
        {
			foreach (KeyValuePair<string, Player> pair in map._players)
			{
				foreach (BaseUnit unit in pair.Value.GetItems())
                {
					if (unit is BaseFood)
                    {
						pair.Value.SetSpeed(pair.Value.Speed + unit.Impact);
					}
                } 
			}

			_nextResolver.Resolve(map);

		}
	}
	
}
