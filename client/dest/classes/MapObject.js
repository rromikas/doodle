import { colors } from "../constants/index.js";
class MapObject {
    constructor(u, type) {
        var _a;
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
        (_a = document.getElementById("map")) === null || _a === void 0 ? void 0 : _a.appendChild(this.node);
    }
}
export default MapObject;
