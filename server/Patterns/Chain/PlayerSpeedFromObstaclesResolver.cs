

using GameServer.Models;
using System.Collections.Generic;
/**
* @(#) FoodResolver.cs
*/
namespace GameServer.Patterns.Chain
{
	public class PlayerSpeedFromObstaclesResolver : AbstractPlayerUpdateResolver
	{
		public override void Resolve(Map map)
        {
			foreach (KeyValuePair<string, Player> pair in map._players)
			{
				foreach (BaseUnit unit in pair.Value.GetItems())
                {
					if (unit is BaseObstacle && pair.Value.Speed > 10)
                    {
						pair.Value.SetSpeed(pair.Value.Speed - unit.Impact);
					}
                } 
			}
		}
	}
	
}
