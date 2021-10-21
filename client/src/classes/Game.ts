import { HubConnection } from "../signalr/HubConnection";
import { getRandomInt } from "../helper.js";
import Player from "./Player.js";
import IMap from "../interfaces/Map.js";
import MapObject from "./MapObject.js";
import BaseFood from "../interfaces/BaseFood.js";

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
  movingInterval: NodeJS.Timeout | undefined;
  screenHeight: number;
  mapHeight: number;
  players: { [username: string]: Player } = {};
  mapObjects: MapObject[] = [];
  initialMapSet: boolean = false;
  mainPlayer: Player;

  constructor(conn: HubConnection) {
    this.connection = conn;
    this.addCommandListeners();
    this.mapNode = document.getElementById("map") as HTMLElement;
    this.screenHeight = window.innerHeight;
    this.mapHeight = parseInt(this.mapNode.style.height);
    this.scoreNode = document.getElementById("score") as HTMLElement;
    this.addJoinListeners();
    this.mainPlayer = new Player(
      { userName: "", size: 0, color: 0, coordinate: { x: 0, y: 0 }, id: "" },
      true
    );

    this.connection.on("PlayersInfo", (map: IMap) => {
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
      let foundIndex = this.mapObjects.findIndex((x) => x.unit.id === unitId);
      if (foundIndex === -1) return;
      let found = this.mapObjects[foundIndex];
      found.node.parentNode?.removeChild(found.node);
      this.mapObjects.splice(foundIndex, 1);
    });
  }

  initializeMap(map: IMap) {
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
    this.mainPlayer = new Player(
      {
        coordinate: { x: getRandomInt(100, 800), y: -100 },
        userName: input.value,
        color: 0,
        size: 80,
        id: Math.random().toString(),
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
  }

  tryEat(foodObject: BaseFood) {
    const { x: fx, y: fy } = foodObject.coordinate;
    const { x: px, y: py } = this.mainPlayer.coordinate;

    if (px < fx && fx < px + this.mainPlayer.size) {
      if (-py < fy && fy < -py + this.mainPlayer.size) {
        this.connection.invoke("eat", this.mainPlayer.userName, foodObject.id);
        this.changeSpeed(this.speed + foodObject._pointReward);
      }
    }
  }

  tryBump(foodObject: BaseFood) {
    const { x: fx, y: fy } = foodObject.coordinate;
    const { x: px, y: py } = this.mainPlayer.coordinate;

    if (px < fx && fx > px + this.mainPlayer.size) {
      if (py < fy && fy > py + this.mainPlayer.size) {
        this.connection.invoke("eat", this.mainPlayer.userName, foodObject.id);
      }
    }
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

  changeSpeed(speed: number) {
    this.speed = speed;
  }

  move() {
    let dx = 0,
      dy = 0,
      mapDy = 0;

    let [px, py] = this.mainPlayer.node.style.transform
      .split("(")[1]
      .split(")")[0]
      .split(",")
      .map((x) => parseInt(x));

    let mapY = parseInt(this.mapNode.style.transform.split("(")[1].split(")")[0]);

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
          let score = parseInt(this.scoreNode.innerHTML);
          this.scoreNode.innerHTML = (score - this.moveValues[x]()).toString();
        } else {
          dx += this.moveValues[x]();
        }
      }
    });
    let newX = px + dx,
      newY = py + dy;

    this.mainPlayer.node.style.transform = `translate(${newX}px, ${newY}px)`;
    this.mapNode.style.transform = `translateY(${mapY + mapDy}px)`;
    this.mainPlayer.coordinate = { x: newX, y: newY - (mapY + mapDy) };
    this.connection.invoke("move", this.mainPlayer.userName, this.mainPlayer.coordinate);
    this.mapObjects.forEach((x) => {
      if (x.type === "food") {
        this.tryEat(x.unit as BaseFood);
      }
    });
  }
}
