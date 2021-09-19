import { HubConnection } from "../signalr/HubConnection";

export class Chat {
  node: HTMLElement;
  connection: HubConnection;

  constructor(conn: HubConnection) {
    this.node = document.getElementById("messages") as HTMLElement;
    this.connection = conn;
    const pingButton = document.getElementById("ping-button") as HTMLElement;
    pingButton.addEventListener("click", () => {
      this.connection.invoke(
        "SendMessage",
        "username",
        `Someone pinged at ${new Date().toISOString().split("T")[1]}`
      );
    });
  }

  addMessage(message: string) {
    const newMessage = document.createElement("div");
    newMessage.className = "message";
    newMessage.innerHTML = message;
    this.node.appendChild(newMessage);
  }
}
