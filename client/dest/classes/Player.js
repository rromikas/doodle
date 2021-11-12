class Player {
    constructor(u, isMain = false) {
        var _a;
        this.items = [];
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
        var _a;
        if (!this.isMain) {
            this.node.style.transform = `translate(${x}px,${y}px)`;
        }
        else {
            const mapY = parseInt(((_a = document.getElementById("map")) === null || _a === void 0 ? void 0 : _a.style.transform.split("(")[1].split(")")[0]) || "0");
            this.node.style.transform = `translate(${x}px, ${y + mapY}px)`;
        }
        this.coordinate = { x, y };
    }
}
export default Player;
