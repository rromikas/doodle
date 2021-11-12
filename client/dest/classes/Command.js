class Robot {
    move(direction) {
        console.log(`Robot moved to ${direction}`);
    }
}
class Command {
    constructor(r) {
        this.robot = r;
    }
    undo() { }
    execute() { }
}
class Left extends Command {
    execute() {
        this.robot.move("left");
    }
    undo() {
        this.robot.move("right");
    }
}
class Right extends Command {
    execute() {
        this.robot.move("right");
    }
    undo() {
        this.robot.move("left");
    }
}
class Forward extends Command {
    execute() {
        this.robot.move("forward");
    }
    undo() {
        this.robot.move("backward");
    }
}
class Backward extends Command {
    execute() {
        this.robot.move("backward");
    }
    undo() {
        this.robot.move("forward");
    }
}
class RemoteController {
    constructor() {
        this.history = [];
    }
    execute(command) {
        this.history.push(command);
        command.execute();
    }
    undo() {
        if (this.history.length) {
            let lastCommand = this.history.pop();
            lastCommand === null || lastCommand === void 0 ? void 0 : lastCommand.undo();
        }
    }
}
export class Application {
    main() {
        const RobotRemoteController = new RemoteController();
        const RobotInstance = new Robot();
        RobotRemoteController.execute(new Left(RobotInstance));
        RobotRemoteController.execute(new Right(RobotInstance));
        RobotRemoteController.execute(new Forward(RobotInstance));
        RobotRemoteController.execute(new Backward(RobotInstance));
        RobotRemoteController.undo();
        RobotRemoteController.undo();
        RobotRemoteController.undo();
        RobotRemoteController.undo();
    }
}
