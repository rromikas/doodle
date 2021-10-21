import IPlayer from "../interfaces/Player";

export type Coordinate = { x: number; y: number };

class Player implements IPlayer {
  userName: string;
  size: number;
  color: 0 | 1 | 2 | 3;
  coordinate: Coordinate;
  node: HTMLElement;
  id: string;

  constructor(unit: IPlayer, isMain: boolean = false) {
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
