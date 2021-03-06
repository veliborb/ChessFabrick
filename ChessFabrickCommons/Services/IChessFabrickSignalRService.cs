﻿using ChessCommons;
using ChessFabrickCommons.Entities;
using ChessFabrickCommons.Models;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessFabrickCommons.Services
{
    public interface IChessFabrickSignalRService : IService
    {
        Task GameCreated(ChessGameInfo game);
        Task PlayerJoined(string playerName, ChessGameInfo game);
        Task PieceMovedAsync(string playerName, string from, string to, ChessGameState game);
    }
}
