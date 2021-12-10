import { Mediator } from "../interfaces/Mediator";

export class Button {
  mediator: Mediator;
  node: HTMLButtonElement;
  constructor(m: Mediator, id: string) {
    this.mediator = m;
    this.node = document.getElementById(id) as HTMLButtonElement;
    this.node.addEventListener("click", () => {
      this.mediator.notify("click");
    });
  }
}
