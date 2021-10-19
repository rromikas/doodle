import BlueFood from "./BlueFood";
import BlueRock from "./BlueRock";
import Island from "./Island";
import Player from "./Player";
import SnowBall from "./SnowBall";

interface IMap {
  _players: { [key: string]: Player };
  _snowBalls: SnowBall[];
  _islands: Island[];
  _blueFoods: BlueFood[];
  _blueRocks: BlueRock[];
}

export default IMap;
