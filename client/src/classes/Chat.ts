import { HubConnection } from "../signalr/HubConnection";
import { Message } from "./Message.js";
import { Mediator } from "../interfaces/Mediator";
import { Input } from "./Input.js";
import { Button } from "./Button.js";
import { Game } from "./Game.js";

export class Chat implements Mediator {
  connection: HubConnection;
  input: Input;
  button: Button;
  messages: Message[] = [];
  node: HTMLElement;
  game: Game;

  constructor(conn: HubConnection, game: Game) {
    this.connection = conn;
    this.game = game;
    this.node = document.getElementById("chat-content") as HTMLElement;
    this.input = new Input(this, "chat-input");
    this.button = new Button(this, "chat-button");
  }

  notify() {
    this.connection.invoke("sendMessage", this.game.mainPlayer.userName, this.input.node.value);
    this.input.node.value = "";
  }

  addMessage(user: string, text: string) {
    this.messages.push(new Message(user, text));
    this.node.scrollTop = 1000000;
  }

  clear() {
    this.node.innerHTML = "";
  }
}
