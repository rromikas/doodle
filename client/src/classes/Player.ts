export type Coordinate = { x: number; y: number };

class Player {
  username: string;
  node: HTMLElement;

  constructor(username: string, coordinate: Coordinate) {
    this.username = username;
    this.node = document.createElement("div");
    this.node.classList.add("otherPlayer");
    document.getElementById("map")?.appendChild(this.node);
    this.setCoordinate(coordinate);
  }

  setCoordinate({ x, y }: Coordinate) {
    this.node.style.transform = `translate(${x}px,${y}px)`;
  }
}

export default Player;
