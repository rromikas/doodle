import { GameLevels } from "../constants/index";
import BaseFood from "./BaseFood";
import BaseObstacle from "./BaseObstacle";
import Composite from "./Composite";
import Island from "./Island";
import Player from "./Player";
import SnowBall from "./SnowBall";
import { Message } from "./Message";

interface IMap {
  gameLevel: keyof typeof GameLevels;
  _players: { [key: string]: Player };
  _snowBalls: SnowBall[];
  _islands: Island[];
  _foods: BaseFood[];
  _rocks: BaseObstacle[];
  _boxes: Composite[];
  _messages: Message[];
}

export default IMap;
