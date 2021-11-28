/**
 * @(#) CasualState.cs
 */

namespace Patterns.State
{
	public class CasualState : PlayerState
	{
        public CasualState(): base(null)
        {
			
        }

		public override int UpdateSpeed(int speed)
		{
			return speed;
		}
		
	}
	
}
