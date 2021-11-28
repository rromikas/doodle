using GameServer.Patterns.State;

namespace GameServer.Models
{
    public class Player : Composite
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public int Speed { get; set; } = 10;
        public PlayerState State {get; set; }
       
        public Player(string userName): base(new Coordinate(), Constants.ColorTypes.Black)
        {
            UserName = userName;
            Size = new Size(50, 50);
            State = new CasualState();
        }

        public void UpdateSpeedByState()
        {
            if (State.GetNextState() != null && State.CheckIfExpired())
            {
                SetNextState();
            }

            SetSpeed(State.GetNewSpeed(Speed));
        }

        private void SetNextState()
        {
            if (State.GetNextState() != null)
            {
                State = State.GetNextState();
            }
           
        }

        public void SetSpeed (int speed)
        {
            Speed = speed;
        }
    }

}
