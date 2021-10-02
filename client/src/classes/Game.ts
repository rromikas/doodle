import { runInThisContext } from "vm";
import { HubConnection } from "../signalr/HubConnection";

type MoveKey = "ArrowDown" | "ArrowUp" | "ArrowLeft" | "ArrowRight";

export class Game {
  connection: HubConnection;
  pressedKeys: { [key in MoveKey]: boolean } = {
    ArrowDown: false,
    ArrowUp: false,
    ArrowLeft: false,
    ArrowRight: false,
  };
  moveValues: { [key in MoveKey]: number } = {
    ArrowDown: 8,
    ArrowUp: -8,
    ArrowLeft: -8,
    ArrowRight: 8,
  };
  playerNode: HTMLElement;
  mapNode: HTMLElement;
  scoreNode: HTMLElement;
  movingInterval: NodeJS.Timeout | undefined;
  screenHeight: number;
  mapHeight: number;
  username: string = "";

  constructor(conn: HubConnection) {
    this.connection = conn;
    this.addCommandListeners();
    this.playerNode = document.getElementById("player") as HTMLElement;
    this.mapNode = document.getElementById("map") as HTMLElement;
    this.screenHeight = window.innerHeight;
    this.mapHeight = parseInt(this.mapNode.style.height);
    this.scoreNode = document.getElementById("score") as HTMLElement;
  }

  join(username: string) {
    this.username = username;
    const usernameField = document.getElementById("username") as HTMLElement;
    // usernameField.innerHTML = username;
    const scoreField = document.getElementById("score") as HTMLElement;
    scoreField.innerHTML = "100";
    const login = document.getElementById("login") as HTMLElement;
    login.style.display = "none";

    this.connection.invoke("SendMessage", username, JSON.stringify({ command: "LOGIN" }));
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

  move() {
    let dx = 0,
      dy = 0,
      mapDy = 0;

    let [px, py] = this.playerNode.style.transform
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
            dy += this.moveValues[x];
          } else {
            mapDy -= this.moveValues[x];
          }
          let score = parseInt(this.scoreNode.innerHTML);
          this.scoreNode.innerHTML = (score - this.moveValues[x]).toString();
        } else {
          dx += this.moveValues[x];
        }
      }
    });
    let newX = px + dx,
      newY = py + dy;
    this.playerNode.style.transform = `translate(${newX}px, ${newY}px)`;
    this.mapNode.style.transform = `translateY(${mapY + mapDy}px)`;
    this.connection.invoke(
      "SendMessage",
      this.username,
      JSON.stringify({ command: "MOVE", data: { x: newX, y: Math.abs(mapY) + newY } })
    );
  }

  login(username: string) {
    this.connection.invoke("SendMessage", username, JSON.stringify({ command: "LOGIN" }));
  }
}
