import Coordinate from "./Coordinate";
import Size from "./Size";

interface BaseUnit {
  color: 0 | 1 | 2 | 3;
  coordinate: Coordinate;
  size: Size;
  id: string;
  impact: number;
}

export default BaseUnit;
