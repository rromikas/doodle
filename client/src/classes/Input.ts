import { Mediator } from "../interfaces/Mediator";

export class Input {
  mediator: Mediator;
  node: HTMLInputElement;
  constructor(m: Mediator, id: string) {
    this.mediator = m;
    this.node = document.getElementById(id) as HTMLInputElement;
    this.node.addEventListener("keydown", (e) => {
      if (e.key === "Enter") {
        this.mediator.notify("");
      }
    });
  }
}
