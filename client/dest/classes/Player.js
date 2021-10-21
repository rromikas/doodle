class Player {
    constructor(unit, isMain = false) {
        var _a;
        this.userName = unit.userName;
        this.size = unit.size;
        this.color = unit.color;
        this.coordinate = unit.coordinate;
        this.id = unit.id;
        if (isMain) {
            this.node = document.getElementById("player");
        }
        else {
            this.node = document.createElement("div");
            this.node.classList.add("otherPlayer");
            (_a = document.getElementById("map")) === null || _a === void 0 ? void 0 : _a.appendChild(this.node);
        }
        this.node.innerHTML = unit.userName;
        this.setCoordinate(unit.coordinate);
    }
    setCoordinate({ x, y }) {
        this.node.style.transform = `translate(${x}px,${y}px)`;
    }
}
export default Player;
