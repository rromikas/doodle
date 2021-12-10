import { Message } from "./Message.js";
import { Input } from "./Input.js";
import { Button } from "./Button.js";
export class Chat {
    constructor(conn, game) {
        this.messages = [];
        this.connection = conn;
        this.game = game;
        this.node = document.getElementById("chat-content");
        this.input = new Input(this, "chat-input");
        this.button = new Button(this, "chat-button");
    }
    notify() {
        this.connection.invoke("sendMessage", this.game.mainPlayer.userName, this.input.node.value);
        this.input.node.value = "";
    }
    addMessage(user, text) {
        this.messages.push(new Message(user, text));
        this.node.scrollTop = 1000000;
    }
    clear() {
        this.node.innerHTML = "";
    }
}
