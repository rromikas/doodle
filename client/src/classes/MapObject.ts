import BaseUnit from "../interfaces/BaseUnit";
import { colors } from "../constants/index.js";

type MapObjectType = "food" | "rock" | "island" | "snowBall" | "box";

class MapObject {
  unit: BaseUnit;
  node: HTMLElement;
  type: MapObjectType;

  constructor(u: BaseUnit, type: MapObjectType) {
    this.type = type;
    this.unit = u;
    this.node = document.createElement("div");
    this.node.classList.add("mapObject");
    this.node.classList.add(type);
    this.node.style.left = `${u.coordinate.x}px`;
    this.node.style.bottom = `${u.coordinate.y}px`;
    this.node.style.background = colors[u.color];
    this.node.style.width = u.size.sizeX + "px";
    this.node.style.height = u.size.sizeY + "px";
    this.node.style.color = "white";
    this.node.innerHTML = type;
    this.node.style.background = colors[u.color];
    document.getElementById("map")?.appendChild(this.node);
  }
}

export default MapObject;
