export class Message {
  user: string;
  text: string;

  constructor(user: string, text: string) {
    this.user = user;
    this.text = text;
    this.render();
  }

  render() {
    const msg = document.createElement("div");
    msg.innerHTML = `<strong>${this.user}</strong>: ` + this.text;
    document.getElementById("chat-content")?.appendChild(msg);
  }
}
