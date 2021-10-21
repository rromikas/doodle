import { colors } from "../constants/index.js";
class MapObject {
    constructor(u, type) {
        var _a;
        this.type = type;
        this.unit = u;
        this.node = document.createElement("div");
        this.node.className = "mapObject";
        this.node.style.borderRadius = "50%";
        this.node.style.left = `${u.coordinate.x}px`;
        this.node.style.bottom = `${u.coordinate.y}px`;
        this.node.style.background = colors[u.color];
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
        (_a = document.getElementById("map")) === null || _a === void 0 ? void 0 : _a.appendChild(this.node);
    }
}
export default MapObject;
