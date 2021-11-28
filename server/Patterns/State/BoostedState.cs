/**
 * @(#) BoostedState.cs
 */

namespace GameServer.Patterns.State
{
	public class BoostedState : PlayerState
	{
        public BoostedState(PlayerState nextState) : base(nextState)
        {

        }

		public override int GetNewSpeed(int speed)
		{
			if (speed > 100) return speed;
			return speed + 2;
		}
	}
	
}
