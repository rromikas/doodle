/**
 * @(#) BoostedState.cs
 */

namespace GameServer.Patterns.State
{
	public class PausedState : PlayerState
	{
        public PausedState(PlayerState nextState) : base(nextState)
        {

        }

		public override int GetNewSpeed(int speed)
		{
			return 0;
		}
	}
	
}
