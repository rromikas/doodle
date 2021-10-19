import { getRandomInt } from "../helper.js";
import Player from "./Player.js";
import MapObject from "./MapObject.js";
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
        this.players = {};
        this.mapObjects = [];
        this.initialMapSet = false;
        this.connection = conn;
        this.addCommandListeners();
        this.playerNode = document.getElementById("player");
        this.mapNode = document.getElementById("map");
        this.screenHeight = window.innerHeight;
        this.mapHeight = parseInt(this.mapNode.style.height);
        this.scoreNode = document.getElementById("score");
        this.addJoinListeners();
        this.connection.on("PlayersInfo", (map) => {
            if (!this.initialMapSet) {
                this.initializeMap(map);
            }
            Object.keys(map._players).forEach((username) => {
                if (!(username in this.players) && username !== this.username) {
                    this.players[username] = new Player(username, map._players[username].coordinate);
                }
            });
        });
        this.connection.on("PlayerMoveInfo", (username, coordinate) => {
            if (username in this.players) {
                this.players[username].setCoordinate(coordinate);
            }
        });
    }
    initializeMap(map) {
        map._blueFoods.forEach((x) => {
            this.mapObjects.push(new MapObject(x, "food"));
        });
        map._blueRocks.forEach((x) => {
            this.mapObjects.push(new MapObject(x, "rock"));
        });
        map._islands.forEach((x) => {
            this.mapObjects.push(new MapObject(x, "island"));
        });
        map._snowBalls.forEach((x) => {
            this.mapObjects.push(new MapObject(x, "snowBall"));
        });
        this.initialMapSet = true;
    }
    addJoinListeners() {
        var _a, _b;
        (_a = document.getElementById("join-btn")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", this.join.bind(this));
        (_b = document.getElementById("username-input")) === null || _b === void 0 ? void 0 : _b.addEventListener("keydown", (e) => {
            if (e.key === "Enter") {
                this.join();
            }
        });
    }
    join() {
        let input = document.querySelector("#username-input");
        this.username = input.value;
        const usernameField = document.getElementById("username");
        // usernameField.innerHTML = username;
        const scoreField = document.getElementById("score");
        scoreField.innerHTML = "100";
        const login = document.getElementById("login");
        login.style.display = "none";
        const coordinate = { x: getRandomInt(100, 600), y: -100 };
        this.playerNode.style.transform = `translate(${coordinate.x}px, ${coordinate.y}px)`;
        this.playerNode.innerHTML = this.username;
        this.connection.invoke("login", this.username, coordinate).catch((error) => {
            console.log("Login error", error);
        });
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
        this.connection.invoke("move", this.username, { x: newX, y: newY - (mapY + mapDy) });
    }
}
