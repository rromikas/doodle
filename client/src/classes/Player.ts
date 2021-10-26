import IPlayer from "../interfaces/Player";
import Size from "../interfaces/Size";

export type Coordinate = { x: number; y: number };

class Player implements IPlayer {
  userName: string;
  size: Size;
  color: 0 | 1 | 2 | 3;
  coordinate: Coordinate;
  node: HTMLElement;
  id: string;
  isMain: boolean;
  impact: number;

  constructor(u: IPlayer | null, isMain: boolean = false) {
    let unit = u || {
      userName: "",
      size: { sizeX: 0, sizeY: 0 },
      color: 0,
      coordinate: { x: 0, y: 0 },
      id: "",
      impact: 0,
    };
    this.userName = unit.userName;
    this.size = unit.size;
    this.color = unit.color;
    this.coordinate = unit.coordinate;
    this.id = unit.id;
    this.isMain = isMain;
    this.impact = unit.impact;

    if (isMain) {
      this.node = document.getElementById("player") as HTMLElement;
    } else {
      this.node = document.createElement("div");
      this.node.classList.add("otherPlayer");
      document.getElementById("map")?.appendChild(this.node);
    }

    this.node.innerHTML = unit.userName;
    this.setCoordinate(unit.coordinate);
  }

  setCoordinate({ x, y }: Coordinate) {
    if (!this.isMain) {
      this.node.style.transform = `translate(${x}px,${y}px)`;
    } else {
      const mapY = parseInt(
        document.getElementById("map")?.style.transform.split("(")[1].split(")")[0] || "0"
      );
      this.node.style.transform = `translate(${x}px, ${y + mapY}px)`;
    }
    this.coordinate = { x, y };
  }
}

export default Player;
