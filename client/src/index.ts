import * as SignalR from "./signalr/index.js";
import { Chat } from "./classes/Chat.js";
import { Game } from "./classes/Game.js";

declare var signalR: typeof SignalR;

let connection = new signalR.HubConnectionBuilder()
  .configureLogging(1)
  .withUrl("http://localhost:5000/gameHub")
  .build();

// const ChatInstance = new Chat(connection);

// connection.on("ReceiveMessage", (user, data) => {
//   ChatInstance.addMessage(data);
// });

const GameInstance = new Game(connection);

const onJoin = () => {
  let input = document.querySelector("#username-input") as HTMLInputElement;
  let username = input.value;
  if (username) {
    GameInstance.join(username);
  }
};
document.getElementById("join-btn")?.addEventListener("click", onJoin);
document.getElementById("username-input")?.addEventListener("keydown", (e) => {
  if (e.key === "Enter") {
    onJoin();
  }
});

connection
  .start()
  .then(() =>
    connection.invoke(
      "SendMessage",
      "username",
      `Someone joined at ${new Date().toISOString().split("T")[1]}`
    )
  )
  .catch((error) => {
    console.log("error starting", error.toString());
  });
