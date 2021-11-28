

using System;
/**
* @(#) PlayerState.cs
*/
namespace Patterns.State
{
	public abstract class PlayerState
	{
		#nullable enable
		PlayerState? _nextState;

		DateTime dateTime;

		public PlayerState(PlayerState? nextState)
        {
            _nextState = nextState;
			dateTime = DateTime.Now;
		}

		public PlayerState? GetNextState()
        {
			return _nextState;
        }
		#nullable disable

		public bool CheckIfExpired()
		{
			if (DateTime.Compare(DateTime.Now, dateTime.AddSeconds(5)) == 1)
            {
				return true;
            }

			return false;
		}

		public abstract int UpdateSpeed(int speed);
	}
}
