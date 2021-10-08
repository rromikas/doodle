var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { Game } from "./classes/Game.js";
const urls = ["http://192.168.1.137:5000/gameHub", "http://localhost:5000/gameHub"];
//builConnection method tries different url to build connection. To some urls can be connected from different machines.
const buildConnection = (urlIndex = 0) => {
    let conn = new signalR.HubConnectionBuilder().configureLogging(1).withUrl(urls[urlIndex]).build();
    return conn
        .start()
        .then(() => {
        console.log("Connection started");
        return conn;
    })
        .catch((error) => {
        console.log("Error while starting connection:", error.toString());
        return buildConnection(urlIndex + 1);
    });
};
const main = () => __awaiter(void 0, void 0, void 0, function* () {
    let connection = yield buildConnection();
    new Game(connection);
});
main();
