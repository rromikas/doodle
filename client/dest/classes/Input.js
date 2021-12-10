export class Input {
    constructor(m, id) {
        this.mediator = m;
        this.node = document.getElementById(id);
        this.node.addEventListener("keydown", (e) => {
            if (e.key === "Enter") {
                this.mediator.notify("");
            }
        });
    }
}
