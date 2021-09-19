export class Chat {
    constructor(conn) {
        this.node = document.getElementById("messages");
        this.connection = conn;
        const pingButton = document.getElementById("ping-button");
        pingButton.addEventListener("click", () => {
            this.connection.invoke("SendMessage", "asdasda", `Someone pinged at ${new Date().toISOString().split("T")[1]}`);
        });
    }
    addMessage(message) {
        const newMessage = document.createElement("div");
        newMessage.className = "message";
        newMessage.innerHTML = message;
        this.node.appendChild(newMessage);
    }
}
