import "./css/main.css";
import * as signalR from "@aspnet/signalr";
import * as React from "react";
import * as ReactDOM from "react-dom";
            
const divMessages: HTMLDivElement = document.querySelector("#divMessages");
const tbMessage: HTMLInputElement = document.querySelector("#tbMessage");
const btnSend: HTMLButtonElement = document.querySelector("#btnSend");
const btnNew: HTMLButtonElement = document.querySelector("#btnNew");

const username = new Date().getTime();

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

connection.start().catch(err => document.write(err));

connection.on("messageReceived", (username: string, message: string) => {
    let m = document.createElement("div");

    m.innerHTML =
        `<div class="message-author">${username}</div><div>${message}</div>`;

    divMessages.appendChild(m);
    divMessages.scrollTop = divMessages.scrollHeight;
});

tbMessage.addEventListener("keyup", (e: KeyboardEvent) => {
    if (e.keyCode === 13) {
        send();
    }
});

btnSend.addEventListener("click", send);
btnNew.addEventListener("click", newMessage);
function send() {
    connection.send("newMessage", username, tbMessage.value)
        .then(() => tbMessage.value = "");
}
function newMessage(){
    var x = new XMLHttpRequest();
    x.open('GET','/SomeDataController/New');
    x.send();
}