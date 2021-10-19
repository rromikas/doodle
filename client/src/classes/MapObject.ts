import Coordinate from "../interfaces/Coordinate";
import BaseUnit from "../interfaces/BaseUnit";

class MapObject {
  unit: BaseUnit;
  node: HTMLElement;

  constructor(u: BaseUnit, type: "food" | "rock" | "island" | "snowBall") {
    this.unit = u;
    this.node = document.createElement("div");
    this.node.className = "mapObject";
    this.node.style.borderRadius = "50%";
    this.node.style.left = `${u.coordinate.x}px`;
    this.node.style.bottom = `${u.coordinate.y}px`;
    this.node.style.background = "blue";
    this.node.style.color = "white";
    switch (type) {
      case "food": {
        this.node.innerHTML = "Food";
        break;
      }
      case "rock": {
        this.node.innerHTML = "Rock";
        break;
      }
      case "island": {
        this.node.innerHTML = "Island";
        this.node.style.background = "brown";
        break;
      }
      case "snowBall": {
        this.node.innerHTML = "SnowBall";
        this.node.style.background = "deepskyblue";
        break;
      }
      default: {
        break;
      }
    }

    document.getElementById("map")?.appendChild(this.node);
  }
}

export default MapObject;
