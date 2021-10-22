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
        this.speed = 10;
        this.moveValues = {
            ArrowDown: () => this.speed,
            ArrowUp: () => -this.speed,
            ArrowLeft: () => -this.speed,
            ArrowRight: () => this.speed,
        };
        this.players = {};
        this.mapObjects = [];
        this.initialMapSet = false;
        this.topReached = false;
        this.connection = conn;
        this.addCommandListeners();
        this.mapNode = document.getElementById("map");
        this.screenHeight = window.innerHeight;
        this.mapHeight = parseInt(this.mapNode.style.height);
        this.scoreNode = document.getElementById("score");
        this.addJoinListeners();
        this.mainPlayer = new Player(null, true);
        this.connection.on("PlayersInfo", (map) => {
            if (!this.initialMapSet) {
                this.initializeMap(map);
            }
            Object.keys(map._players).forEach((username) => {
                if (!(username in this.players) && username !== this.mainPlayer.userName) {
                    this.players[username] = new Player(map._players[username]);
                }
            });
        });
        this.connection.on("PlayerMoveInfo", (username, coordinate) => {
            if (username in this.players) {
                this.players[username].setCoordinate(coordinate);
            }
        });
        this.connection.on("RemoveUnit", (unitId) => {
            var _a;
            let foundIndex = this.mapObjects.findIndex((x) => x.unit.id === unitId);
            if (foundIndex === -1)
                return;
            let found = this.mapObjects[foundIndex];
            (_a = found.node.parentNode) === null || _a === void 0 ? void 0 : _a.removeChild(found.node);
            this.mapObjects.splice(foundIndex, 1);
        });
    }
    initializeMap(map) {
        console.log("MAP", map);
        map._foods.forEach((x) => {
            this.mapObjects.push(new MapObject(x, "food"));
        });
        map._rocks.forEach((x) => {
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
        const scoreField = document.getElementById("score");
        scoreField.innerHTML = "100";
        const login = document.getElementById("login");
        login.style.display = "none";
        this.mainPlayer = new Player({
            coordinate: { x: getRandomInt(100, 800), y: -100 },
            userName: input.value,
            color: 0,
            size: { sizeX: 80, sizeY: 80 },
            id: Math.random().toString(),
        }, true);
        this.connection
            .invoke("login", this.mainPlayer.userName, this.mainPlayer.coordinate)
            .catch((error) => {
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
    eat(foodObject) {
        this.connection.invoke("eat", this.mainPlayer.userName, foodObject.id);
        this.changeSpeed(this.speed + foodObject._pointReward);
    }
    bump(obstacle) { }
    doObjectsOverlap(unit, coordinate) {
        const { x: ux, y: uy } = unit.coordinate;
        const { x: px, y: py } = coordinate;
        let bumped = false;
        let dx = 0;
        let dy = 0;
        if (px < ux + unit.size.sizeX && ux < px + this.mainPlayer.size.sizeX) {
            if (-py < uy + unit.size.sizeY && uy < -py + this.mainPlayer.size.sizeY) {
                bumped = true;
                dx = px > ux ? ux + unit.size.sizeX - px : ux - (px + this.mainPlayer.size.sizeX);
                dy = -py > uy ? uy + unit.size.sizeY + py : uy - (-py + this.mainPlayer.size.sizeY);
                dx = Math.abs(dx) > Math.abs(dy) ? 0 : dx;
                dy = dx === 0 ? dy : 0;
            }
        }
        return { bumped, dx, dy };
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
    changeSpeed(speed) {
        this.speed = speed;
    }
    move() {
        if (!this.topReached) {
            let dx = 0, dy = 0, mapDy = 0;
            let [px, py] = this.mainPlayer.node.style.transform
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
                            dy += this.moveValues[x]();
                        }
                        else {
                            mapDy -= this.moveValues[x]();
                        }
                    }
                    else {
                        dx += this.moveValues[x]();
                    }
                }
            });
            let newX = px + dx;
            let newY = py + dy;
            let potentialX = newX;
            let potentialY = newY - (mapY + mapDy);
            let change = { x: 0, y: 0 };
            this.mapObjects.forEach((x) => {
                let { bumped, dx: changeX, dy: changeY, } = this.doObjectsOverlap(x.unit, { x: potentialX, y: potentialY });
                if (bumped)
                    if (x.type === "food") {
                        this.eat(x.unit);
                    }
                    else {
                        change = { x: changeX, y: changeY };
                        this.bump(x.unit);
                    }
            });
            this.mainPlayer.node.style.transform = `translate(${newX + change.x}px, ${newY + (dy ? -change.y : 0)}px)`;
            this.mapNode.style.transform = `translateY(${mapY + mapDy + (mapDy ? change.y : 0)}px)`;
            this.mainPlayer.coordinate = { x: potentialX + change.x, y: potentialY + change.y };
            this.scoreNode.innerHTML = (-potentialY + change.y).toString();
            this.connection.invoke("move", this.mainPlayer.userName, this.mainPlayer.coordinate);
            if (-this.mainPlayer.coordinate.y > 9500) {
                alert("Congratulations! The top reached!");
                this.topReached = true;
            }
        }
    }
}
