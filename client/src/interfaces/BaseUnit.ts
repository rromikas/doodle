import Coordinate from "./Coordinate";

interface BaseUnit {
  color: 0 | 1 | 2 | 3;
  coordinate: Coordinate;
  size: number;
  id: string;
}

export default BaseUnit;
