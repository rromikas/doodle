export class Message {
    constructor(user, text) {
        this.user = user;
        this.text = text;
        this.render();
    }
    render() {
        var _a;
        const msg = document.createElement("div");
        msg.innerHTML = `<strong>${this.user}</strong>: ` + this.text;
        (_a = document.getElementById("chat-content")) === null || _a === void 0 ? void 0 : _a.appendChild(msg);
    }
}
