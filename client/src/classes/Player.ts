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

  constructor(u: IPlayer | null, isMain: boolean = false) {
    let unit = u || {
      userName: "",
      size: { sizeX: 0, sizeY: 0 },
      color: 0,
      coordinate: { x: 0, y: 0 },
      id: "",
    };
    this.userName = unit.userName;
    this.size = unit.size;
    this.color = unit.color;
    this.coordinate = unit.coordinate;
    this.id = unit.id;

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
    this.node.style.transform = `translate(${x}px,${y}px)`;
  }
}

export default Player;
