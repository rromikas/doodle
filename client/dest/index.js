var _a, _b;
import { Game } from "./classes/Game.js";
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
    let input = document.querySelector("#username-input");
    let username = input.value;
    if (username) {
        GameInstance.join(username);
    }
};
(_a = document.getElementById("join-btn")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", onJoin);
(_b = document.getElementById("username-input")) === null || _b === void 0 ? void 0 : _b.addEventListener("keydown", (e) => {
    if (e.key === "Enter") {
        onJoin();
    }
});
connection
    .start()
    .then(() => connection.invoke("SendMessage", "username", `Someone joined at ${new Date().toISOString().split("T")[1]}`))
    .catch((error) => {
    console.log("error starting", error.toString());
});
