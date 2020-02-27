﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub/chat").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
document.getElementById("testButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("testButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("testButton").addEventListener("click", function (event) {
    connection.invoke("GetTest").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});