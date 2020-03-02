"use strict";

function initConnection(token) {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub/chess", { accessTokenFactory: () => token })
        .build();

    document.getElementById("joinButton").disabled = true;
    document.getElementById("sendButton").disabled = true;

    connection.on("Test", function (message) {
        var encodedMsg = "Test: " + message;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("OnError", function (message) {
        console.log(message);
    });

    connection.on("ShowPieceMoves", function (field, moves) {
        console.log(field + " " + moves);
        var encodedMsg = "Game created: " + JSON.stringify(game);
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("OnGameCreated", function (game) {
        console.log(game);
        var encodedMsg = "Game created: " + JSON.stringify(game);
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("OnPlayerJoined", function (game, user) {
        console.log(game + " " + user);
        var encodedMsg = "User joined: " + JSON.stringify(game) + " " + user;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("OnBoardChanged", function (board) {
        console.log(board);
        var encodedMsg = JSON.stringify(board);
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("OnPieceMoved", function (from, to, board) {
        console.log(from + " " + to + " " + board);
        var encodedMsg = "Piece moved: " + from + " " + to + " " + JSON.stringify(board);
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.start().then(function () {
        document.getElementById("joinButton").disabled = false;
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    return connection;
}

var connection;

document.getElementById("joinButton").addEventListener("click", function (event) {
    document.getElementById("joinButton").disabled = true;
    document.getElementById("sendButton").disabled = true;
    var token = document.getElementById("inputMethod").value;
    connection = initConnection(token);
    event.preventDefault();
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var method = document.getElementById("inputMethod").value;
    var i1 = document.getElementById("input1").value;
    var i2 = document.getElementById("input2").value;
    var i3 = document.getElementById("input3").value;
    console.log(method + " " + i1 + " " + i2 + " " + i3);
    if (i1 == "") {
        connection.invoke(method).catch(function (err) {
            return console.error(err.toString());
        });
    } else if (i2 == "") {
        connection.invoke(method, i1).catch(function (err) {
            return console.error(err.toString());
        });
    } else if (i3 == "") {
        connection.invoke(method, i1, i2).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        connection.invoke(method, i1, i2, i3).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});