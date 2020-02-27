"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub/chess").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
document.getElementById("testButton").disabled = true;

connection.on("Test", function (message) {
    var encodedMsg = "Test: " + message;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("PlayerJoined", function (gameId, playerId) {
    var encodedMsg = playerId + " joined " + gameId;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("GameCreated", function (game) {
    var encodedMsg = "Game created: " + JSON.stringify(game);
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
    var token = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection = new signalR.HubConnectionBuilder().withUrl("/hub/chess", { accessTokenFactory: () => token }).build()
    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
        document.getElementById("testButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("testButton").addEventListener("click", function (event) {
    connection.invoke("GetSecret").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});