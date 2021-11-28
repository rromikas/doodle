/**
 * @(#) SlowedDownState.cs
 */

namespace GameServer.Patterns.State
{
	public class SlowedDownState : PlayerState
	{
		public SlowedDownState(PlayerState nextState) : base(nextState)
		{

		}

		public override int GetNewSpeed(int speed)
		{
			if (speed < 10) return speed;

			return speed-2;
		}

	}
	
}
