import Composite from "./Composite";

interface Player extends Composite {
  userName: string;
  speed: number
}

export default Player;
