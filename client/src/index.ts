import * as SignalR from "./signalr/index.js";
import { Game } from "./classes/Game.js";

declare var signalR: typeof SignalR;

const urls = ["http://192.168.1.137:5000/gameHub", "http://localhost:5000/gameHub"];

//builConnection method tries different url to build connection. To some urls can be connected from different machines.
const buildConnection = (urlIndex: number = 0): Promise<SignalR.HubConnection> => {
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

const main = async () => {
  let connection = await buildConnection();
  new Game(connection);
};

main();
