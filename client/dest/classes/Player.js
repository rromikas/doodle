class Player {
    constructor(username, coordinate) {
        var _a;
        this.username = username;
        this.node = document.createElement("div");
        this.node.classList.add("otherPlayer");
        (_a = document.getElementById("map")) === null || _a === void 0 ? void 0 : _a.appendChild(this.node);
        this.setCoordinate(coordinate);
    }
    setCoordinate({ x, y }) {
        this.node.style.transform = `translate(${x}px,${y}px)`;
    }
}
export default Player;
