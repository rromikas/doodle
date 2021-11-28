/**
 * @(#) CasualState.cs
 */

namespace GameServer.Patterns.State
{
	public class CasualState : PlayerState
	{
        public CasualState(): base(null)
        {
			
        }

		public override int GetNewSpeed(int speed)
		{
			return speed;
		}
		
	}
	
}
