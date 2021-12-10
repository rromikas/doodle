export class Button {
    constructor(m, id) {
        this.mediator = m;
        this.node = document.getElementById(id);
        this.node.addEventListener("click", () => {
            this.mediator.notify("click");
        });
    }
}
