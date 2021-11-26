import { HubConnection } from "../signalr/HubConnection";
import { getRandomInt } from "../helper.js";
import Player from "./Player.js";
import IMap from "../interfaces/Map.js";
import MapObject from "./MapObject.js";
import BaseFood from "../interfaces/BaseFood.js";
import BaseObstacle from "../interfaces/BaseObstacle";
import Coordinate from "../interfaces/Coordinate";
import BaseUnit from "../interfaces/BaseUnit";
import { colors, GameLevels } from "../constants/index.js";
import Composite from "../interfaces/Composite";

type MoveKey = "ArrowDown" | "ArrowUp" | "ArrowLeft" | "ArrowRight";

export class Game {
  connection: HubConnection;
  pressedKeys: { [key in MoveKey]: boolean } = {
    ArrowDown: false,
    ArrowUp: false,
    ArrowLeft: false,
    ArrowRight: false,
  };
  speed = 10;
  moveValues: { [key in MoveKey]: () => number } = {
    ArrowDown: () => this.speed,
    ArrowUp: () => -this.speed,
    ArrowLeft: () => -this.speed,
    ArrowRight: () => this.speed,
  };
  mapNode: HTMLElement;
  scoreNode: HTMLElement;
  speedNode: HTMLElement;
  levelNode: HTMLElement;
  pauseBtn: HTMLElement;
  undoBtn: HTMLElement;
  itemsNode: HTMLElement;
  movingInterval: NodeJS.Timeout | undefined;
  screenHeight: number;
  mapHeight: number;
  players: Player[] = [];
  mapObjects: MapObject[] = [];
  initialMapSet: boolean = false;
  mainPlayer: Player;
  topReached: boolean = false;
  paused: boolean = false;
  lavel: keyof typeof GameLevels | undefined;
  freezed: boolean = false;

  constructor(conn: HubConnection) {
    this.connection = conn;
    this.addCommandListeners();
    this.mapNode = document.getElementById("map") as HTMLElement;
    this.screenHeight = window.innerHeight;
    this.mapHeight = parseInt(this.mapNode.style.height);
    this.scoreNode = document.getElementById("score") as HTMLElement;
    this.scoreNode.innerHTML = "100";
    this.speedNode = document.getElementById("speed") as HTMLElement;
    this.speedNode.innerHTML = this.speed.toString();
    this.levelNode = document.getElementById("level") as HTMLElement;
    this.pauseBtn = document.getElementById("pause-btn") as HTMLElement;
    this.undoBtn = document.getElementById("undo-btn") as HTMLElement;
    this.itemsNode = document.getElementById("items-holder") as HTMLElement;
    this.addJoinListeners();
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
    this.connection.on("UserConnected", this.onUserConnected.bind(this));
    this.connection.on("SetLevel", this.onSetLevel.bind(this));
  }

  onAllInfo(map: IMap) {
    console.log("ALL INFO", map);
    this.rerenderMapObjects(map);
    this.rerenderPlayers(map);
  }

  startRenderingItems() {
    this.itemsNode.innerHTML = "";
    this.renderItems(this.mainPlayer.items, this.itemsNode);
  }

  renderItems(items: BaseUnit[], container: HTMLElement) {
    items.forEach((x) => {
      if (x.items) {
        let composite = x as Composite;
        let packDiv = document.createElement("div");
        packDiv.className = "pack";
        container.appendChild(packDiv);
        this.renderItems(composite.items, packDiv);
      } else {
        let div = document.createElement("div");
        div.classList.add("item");
        div.style.background = colors[x.color];
        div.style.width = x.size.sizeX + "px";
        div.style.height = x.size.sizeY + "px";
        container.appendChild(div);
      }
    });
  }

  async onUserConnected(gameLevel: keyof typeof GameLevels | null) {
    if (gameLevel == null) {
      const res = await window.prompt("Select game level (0,1,2)");
      if (res) {
        this.connection.invoke("setLevel", +res);
      }
    } else {
      this.onSetLevel(gameLevel);
    }
  }

  onSetLevel(gameLevel: keyof typeof GameLevels) {
    this.lavel = gameLevel;
    this.levelNode.innerHTML = GameLevels[gameLevel];
  }

  rerenderMapObjects(map: IMap) {
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

  onMoveObstacles(map: IMap) {
    this.rerenderMapObjects(map);
  }

  onLogout(username: string) {
    if (this.mainPlayer.userName === username) {
      window.location.reload();
    } else {
      this.onRemovePlayer(username);
    }
  }

  onUndo() {
    this.connection.invoke("undo", this.mainPlayer.userName);
  }

  onRemovePlayer(username: string) {
    let index = this.players.findIndex((x) => x.userName === username);
    if (index !== -1) {
      let node = this.players[index].node;
      node.parentNode?.removeChild(node);
      this.players.splice(index, 1);
    }
  }

  onRemoveUnit(unitId: string) {
    let foundIndex = this.mapObjects.findIndex((x) => x.unit.id === unitId);
    if (foundIndex === -1) return;
    let found = this.mapObjects[foundIndex];
    this.mapObjects.splice(foundIndex, 1);
    found.node.parentNode?.removeChild(found.node);
  }

  onAddUnit(unit: BaseUnit, username: string) {
    this.mapObjects.push(new MapObject(unit, "food"));
    if (username === this.mainPlayer.userName) {
      this.changeSpeed(-unit.impact);
    }
  }

  onPlayerMoveInfo(username: string, coordinate: Coordinate, undo: boolean) {
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

  onPlayersInfo(map: IMap) {
    if (!this.initialMapSet) {
      this.initializeMap(map);
    }
    this.rerenderPlayers(map);
  }

  rerenderPlayers(map: IMap) {
    this.players.forEach((x) => {
      this.onRemovePlayer(x.userName);
    });

    Object.keys(map._players).forEach((username) => {
      let player = map._players[username];
      if (username !== this.mainPlayer.userName) {
        this.players.push(new Player(player));
      } else {
        this.mainPlayer.items = player.items;
        this.startRenderingItems();
      }
    });
  }

  initializeMap(map: IMap) {
    this.rerenderMapObjects(map);
    this.initialMapSet = true;
  }

  addJoinListeners() {
    document.getElementById("join-btn")?.addEventListener("click", this.join.bind(this));
    document.getElementById("username-input")?.addEventListener("keydown", (e) => {
      if (e.key === "Enter") {
        this.join();
      }
    });
  }

  join() {
    let input = document.querySelector("#username-input") as HTMLInputElement;
    const scoreField = document.getElementById("score") as HTMLElement;
    scoreField.innerHTML = "100";
    const login = document.getElementById("login") as HTMLElement;
    login.style.display = "none";
    let initialCoordinate = { x: getRandomInt(100, 800), y: -100 };
    this.mainPlayer = new Player(
      {
        coordinate: initialCoordinate,
        userName: input.value,
        color: 0,
        size: { sizeX: 80, sizeY: 80 },
        id: Math.random().toString(),
        impact: 0,
        items: [],
      },
      true
    );

    this.connection
      .invoke("login", this.mainPlayer.userName, this.mainPlayer.coordinate)
      .catch((error) => {
        console.log("Login error", error);
      });
  }

  addCommandListeners() {
    window.addEventListener("keydown", (e) => {
      if (Object.keys(this.pressedKeys).includes(e.key)) {
        this.pressedKeys[e.key as MoveKey] = true;
        if (!this.movingInterval) {
          this.startMoving();
        }
      }
    });

    window.addEventListener("keyup", (e) => {
      if (Object.keys(this.pressedKeys).includes(e.key)) {
        this.pressedKeys[e.key as MoveKey] = false;
      }
      if (!Object.values(this.pressedKeys).find((x) => x)) {
        this.stopMoving();
      }
    });

    document.getElementById("undo-btn")?.addEventListener("click", this.onUndo.bind(this));
    document
      .getElementById("pause-btn")
      ?.addEventListener("click", this.invokePauseResume.bind(this));
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
    } else {
      this.connection.invoke("undo", this.mainPlayer.userName);
    }
  }

  eat(foodObject: BaseFood) {
    this.connection.invoke("eat", this.mainPlayer.userName, foodObject.id);
    this.changeSpeed(foodObject.impact);
  }

  bump(obstacle: BaseObstacle) {}

  openBox(box: BaseUnit) {
    this.connection.invoke("openBox", this.mainPlayer.userName, box.id);
    box.items?.forEach((x) => {
      this.changeSpeed(x.impact);
    });
  }

  doObjectsOverlap(
    unit: BaseUnit,
    coordinate: Coordinate
  ): { bumped: boolean; dx: number; dy: number } {
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
    if (!this.movingInterval) return;
    clearInterval(this.movingInterval);
    this.movingInterval = undefined;
  }

  changeSpeed(speedChange: number) {
    this.speed = this.speed + speedChange;
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
      let dx = 0,
        dy = 0,
        mapDy = 0;

      let [px, py] = this.getMainPlayerXY();

      let mapY = this.getMapOffsetY();

      (Object.keys(this.pressedKeys) as MoveKey[]).forEach((x, i) => {
        if (this.pressedKeys[x]) {
          if (i < 2) {
            if (
              (this.mapHeight - mapY <= this.screenHeight && i === 1) ||
              (mapY <= 0 && i === 0) ||
              py > -200 ||
              py < -350
            ) {
              dy += this.moveValues[x]();
            } else {
              mapDy -= this.moveValues[x]();
            }
          } else {
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
        let {
          bumped,
          dx: changeX,
          dy: changeY,
        } = this.doObjectsOverlap(x.unit, { x: potentialX, y: potentialY });
        if (bumped && !this.freezed) {
          if (x.type === "food") {
            this.eat(x.unit as BaseFood);
          } else if (x.type === "box") {
            this.openBox(x.unit);
          } else {
            change = { x: changeX, y: changeY };
            this.bump(x.unit as BaseObstacle);
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

      this.mainPlayer.node.style.transform = `translate(${newX + change.x}px, ${
        newY + (dy ? -change.y : 0)
      }px)`;
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
