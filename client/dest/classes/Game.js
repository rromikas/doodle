export class Game {
    constructor(conn) {
        this.pressedKeys = {
            ArrowDown: false,
            ArrowUp: false,
            ArrowLeft: false,
            ArrowRight: false,
        };
        this.moveValues = {
            ArrowDown: 8,
            ArrowUp: -8,
            ArrowLeft: -8,
            ArrowRight: 8,
        };
        this.username = "";
        this.connection = conn;
        this.addCommandListeners();
        this.playerNode = document.getElementById("player");
        this.mapNode = document.getElementById("map");
        this.screenHeight = window.innerHeight;
        this.mapHeight = parseInt(this.mapNode.style.height);
        this.scoreNode = document.getElementById("score");
    }
    join(username) {
        this.username = username;
        const usernameField = document.getElementById("username");
        // usernameField.innerHTML = username;
        const scoreField = document.getElementById("score");
        scoreField.innerHTML = "100";
        const login = document.getElementById("login");
        login.style.display = "none";
        this.connection.invoke("SendMessage", username, { command: "LOGIN" });
    }
    addCommandListeners() {
        window.addEventListener("keydown", (e) => {
            if (Object.keys(this.pressedKeys).includes(e.key)) {
                this.pressedKeys[e.key] = true;
                if (!this.movingInterval) {
                    this.startMoving();
                }
            }
        });
        window.addEventListener("keyup", (e) => {
            if (Object.keys(this.pressedKeys).includes(e.key)) {
                this.pressedKeys[e.key] = false;
            }
            if (!Object.values(this.pressedKeys).find((x) => x)) {
                this.stopMoving();
            }
        });
    }
    startMoving() {
        this.movingInterval = setInterval(() => {
            this.move();
        }, 50);
    }
    stopMoving() {
        if (!this.movingInterval)
            return;
        clearInterval(this.movingInterval);
        this.movingInterval = undefined;
    }
    move() {
        let dx = 0, dy = 0, mapDy = 0;
        let [px, py] = this.playerNode.style.transform
            .split("(")[1]
            .split(")")[0]
            .split(",")
            .map((x) => parseInt(x));
        let mapY = parseInt(this.mapNode.style.transform.split("(")[1].split(")")[0]);
        Object.keys(this.pressedKeys).forEach((x, i) => {
            if (this.pressedKeys[x]) {
                if (i < 2) {
                    if ((this.mapHeight - mapY <= this.screenHeight && i === 1) ||
                        (mapY <= 0 && i === 0) ||
                        py > -200 ||
                        py < -350) {
                        dy += this.moveValues[x];
                    }
                    else {
                        mapDy -= this.moveValues[x];
                    }
                    let score = parseInt(this.scoreNode.innerHTML);
                    this.scoreNode.innerHTML = (score - this.moveValues[x]).toString();
                }
                else {
                    dx += this.moveValues[x];
                }
            }
        });
        let newX = px + dx, newY = py + dy;
        this.playerNode.style.transform = `translate(${newX}px, ${newY}px)`;
        this.mapNode.style.transform = `translateY(${mapY + mapDy}px)`;
        this.connection.invoke("SendMessage", this.username, JSON.stringify({ command: "MOVE", data: { x: newX, y: Math.abs(mapY) + newY } }));
    }
    login(username) {
        this.connection.invoke("SendMessage", username, JSON.stringify({ command: "LOGIN" }));
    }
}
