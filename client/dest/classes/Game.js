import { getRandomInt } from "../helper.js";
import Player from "./Player.js";
import MapObject from "./MapObject.js";
import { colors } from "../constants/index.js";
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
        this.players = [];
        this.mapObjects = [];
        this.initialMapSet = false;
        this.topReached = false;
        this.paused = false;
        this.freezed = false;
        this.connection = conn;
        this.mapNode = document.getElementById("map");
        this.screenHeight = window.innerHeight;
        this.mapHeight = parseInt(this.mapNode.style.height);
        this.scoreNode = document.getElementById("score");
        this.scoreNode.innerHTML = "100";
        this.speedNode = document.getElementById("speed");
        this.speedNode.innerHTML = this.speed.toString();
        this.levelNode = document.getElementById("levels-container");
        this.pauseBtn = document.getElementById("pause-btn");
        this.undoBtn = document.getElementById("undo-btn");
        this.itemsNode = document.getElementById("items-holder");
        this.addJoinListeners();
        this.addCommandListeners();
        this.mainPlayer = new Player(null, true);
        window.setInterval(() => this.connection.invoke("updateMap"), 500);
        this.connection.on("AllInfo", this.onAllInfo.bind(this));
        this.connection.on("PlayersInfo", this.onPlayersInfo.bind(this));
        this.connection.on("PlayerMoveInfo", this.onPlayerMoveInfo.bind(this));
        this.connection.on("RemoveUnit", this.onRemoveUnit.bind(this));
        this.connection.on("AddUnit", this.onAddUnit.bind(this));
        this.connection.on("Logout", this.onLogout.bind(this));
        this.connection.on("Pause", this.onPause.bind(this));
        this.connection.on("Resume", this.onResume.bind(this));
        this.connection.on("MoveObstacles", this.onMoveObstacles.bind(this));
    }
    onAllInfo(map) {
        console.log("ALL INFO", map);
        this.rerenderMapObjects(map);
        this.rerenderPlayers(map);
    }
    startRenderingItems() {
        this.itemsNode.innerHTML = "";
        this.renderItems(this.mainPlayer.items, this.itemsNode);
    }
    renderItems(items, container) {
        items.forEach((x) => {
            if (x.items) {
                let composite = x;
                let packDiv = document.createElement("div");
                packDiv.className = "pack";
                container.appendChild(packDiv);
                this.renderItems(composite.items, packDiv);
            }
            else {
                let div = document.createElement("div");
                div.classList.add("item");
                div.style.background = colors[x.color];
                div.style.width = x.size.sizeX + "px";
                div.style.height = x.size.sizeY + "px";
                container.appendChild(div);
            }
        });
    }
    updateLevelButtons(gameLevel) {
        let lvlButtons = this.levelNode.children;
        console.log("GAME LEVEL, level buttons", gameLevel, lvlButtons.length);
        for (let i = 0; i < lvlButtons.length; i++) {
            lvlButtons[i].classList.remove("active");
            if (i === gameLevel - 1) {
                console.log("buvo");
                lvlButtons[i].classList.add("active");
            }
        }
    }
    rerenderMapObjects(map) {
        this.updateLevelButtons(map.gameLevel);
        this.mapObjects.forEach((x) => this.onRemoveUnit(x.unit.id));
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
        map._boxes.forEach((x) => {
            this.mapObjects.push(new MapObject(x, "box"));
        });
    }
    onMoveObstacles(map) {
        this.rerenderMapObjects(map);
    }
    onLogout(username) {
        if (this.mainPlayer.userName === username) {
            window.location.reload();
        }
        else {
            this.onRemovePlayer(username);
        }
    }
    onUndo() {
        this.connection.invoke("undo", this.mainPlayer.userName);
    }
    onRemovePlayer(username) {
        var _a;
        let index = this.players.findIndex((x) => x.userName === username);
        if (index !== -1) {
            let node = this.players[index].node;
            (_a = node.parentNode) === null || _a === void 0 ? void 0 : _a.removeChild(node);
            this.players.splice(index, 1);
        }
    }
    onRemoveUnit(unitId) {
        var _a;
        let foundIndex = this.mapObjects.findIndex((x) => x.unit.id === unitId);
        if (foundIndex === -1)
            return;
        let found = this.mapObjects[foundIndex];
        this.mapObjects.splice(foundIndex, 1);
        (_a = found.node.parentNode) === null || _a === void 0 ? void 0 : _a.removeChild(found.node);
    }
    onAddUnit(unit) {
        this.mapObjects.push(new MapObject(unit, "food"));
    }
    onPlayerMoveInfo(username, coordinate, undo) {
        if (username in this.players) {
            let index = this.players.findIndex((x) => x.userName === username);
            if (index !== -1) {
                this.players[index].setCoordinate(coordinate);
            }
        }
        if (username === this.mainPlayer.userName && undo) {
            this.mainPlayer.setCoordinate(coordinate);
        }
    }
    onPlayersInfo(map) {
        if (!this.initialMapSet) {
            this.initializeMap(map);
        }
        this.rerenderPlayers(map);
    }
    rerenderPlayers(map) {
        this.players.forEach((x) => {
            this.onRemovePlayer(x.userName);
        });
        Object.keys(map._players).forEach((username) => {
            let player = map._players[username];
            if (username !== this.mainPlayer.userName) {
                this.players.push(new Player(player));
            }
            else {
                this.mainPlayer.items = player.items;
                this.changeSpeed(player.speed);
                this.startRenderingItems();
            }
        });
    }
    initializeMap(map) {
        this.rerenderMapObjects(map);
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
        let initialCoordinate = { x: getRandomInt(100, 800), y: -100 };
        this.mainPlayer = new Player({
            coordinate: initialCoordinate,
            userName: input.value,
            color: 0,
            size: { sizeX: 80, sizeY: 80 },
            id: Math.random().toString(),
            impact: 0,
            items: [],
            speed: 10,
        }, true);
        this.connection
            .invoke("login", this.mainPlayer.userName, this.mainPlayer.coordinate)
            .catch((error) => {
            console.log("Login error", error);
        });
    }
    addCommandListeners() {
        var _a, _b;
        let lvlButtons = this.levelNode.children;
        for (let i = 0; i < lvlButtons.length; i++) {
            lvlButtons[i].addEventListener("click", (e) => {
                this.connection.invoke("setLevel", this.mainPlayer.userName, i + 1);
            });
        }
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
        (_a = document.getElementById("undo-btn")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", this.onUndo.bind(this));
        (_b = document
            .getElementById("pause-btn")) === null || _b === void 0 ? void 0 : _b.addEventListener("click", this.invokePauseResume.bind(this));
    }
    onPause() {
        this.paused = true;
        this.pauseBtn.innerHTML = "Resume";
    }
    onResume() {
        this.paused = false;
        this.pauseBtn.innerHTML = "Pause";
    }
    invokePauseResume() {
        if (!this.paused) {
            this.connection.invoke("pause", this.mainPlayer.userName);
        }
        else {
            this.connection.invoke("undo", this.mainPlayer.userName);
        }
    }
    eat(foodObject) {
        this.connection.invoke("eat", this.mainPlayer.userName, foodObject.id);
    }
    bump(obstacle) { }
    openBox(box) {
        this.connection.invoke("openBox", this.mainPlayer.userName, box.id);
    }
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
    changeSpeed(newSpeed) {
        this.speed = newSpeed;
        this.speedNode.innerHTML = this.speed.toString();
    }
    getMapOffsetY() {
        return parseInt(this.mapNode.style.transform.split("(")[1].split(")")[0]);
    }
    getMainPlayerXY() {
        return this.mainPlayer.node.style.transform
            .split("(")[1]
            .split(")")[0]
            .split(",")
            .map((x) => parseInt(x));
    }
    move() {
        if (!this.topReached && !this.paused) {
            let dx = 0, dy = 0, mapDy = 0;
            let [px, py] = this.getMainPlayerXY();
            let mapY = this.getMapOffsetY();
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
                if (bumped && !this.freezed) {
                    if (x.type === "food") {
                        this.eat(x.unit);
                    }
                    else if (x.type === "box") {
                        this.openBox(x.unit);
                    }
                    else {
                        change = { x: changeX, y: changeY };
                        this.bump(x.unit);
                    }
                    //Freezing reikalingas, kad to pačio itemo nevalgytu kelis kartus, kol per serveri suvaiksto duomenys. Jei kazka geriau sugalvosit pakeiskit.
                    if (["food", "box"].includes(x.type)) {
                        this.freezed = true;
                        setTimeout(() => {
                            this.freezed = false;
                        }, 500);
                    }
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
