import { Chat } from "./classes/Chat.js";
let connection = new signalR.HubConnectionBuilder()
    .configureLogging(1)
    .withUrl("http://localhost:5000/gameHub")
    .build();
const ChatInstance = new Chat(connection);
connection.on("ReceiveMessage", (user, data) => {
    console.log(data);
    ChatInstance.addMessage(data);
});
connection
    .start()
    .then(() => connection.invoke("SendMessage", "Asdasdasd", `Someone joined at ${new Date().toISOString().split("T")[1]}`))
    .catch((error) => {
    console.log("error starting", error.toString());
});
