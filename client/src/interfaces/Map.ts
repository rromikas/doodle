import BaseFood from "./BaseFood";
import BaseObstacle from "./BaseObstacle";
import Island from "./Island";
import Player from "./Player";
import SnowBall from "./SnowBall";

interface IMap {
  _players: { [key: string]: Player };
  _snowBalls: SnowBall[];
  _islands: Island[];
  _foods: BaseFood[];
  _rocks: BaseObstacle[];
}

export default IMap;
