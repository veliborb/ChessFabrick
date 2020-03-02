using ChessCommons;
using ChessFabrickCommons.Entities;
using ChessFabrickCommons.Models;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessFabrickCommons.Services
{
    public interface IChessFabrickStatefulService : IService
    {
        Task<string> HelloChessAsync();
        Task<ChessPlayer> NewPlayerAsync(string name);
        Task<ChessPlayer> PlayerInfoAsync(string playerName);
        Task<ChessGameInfo> NewGameAsync(string playerName, PieceColor playerColor);
        Task<ChessGameInfo> JoinGameAsync(string playerName, long gameId);
        Task<ChessGameState> GameStateAsync(long gameId);
        Task<List<string>> ListPieceMovesAsync(long gameId, string from);
        Task<ChessGameState> MovePieceAsync(string playerName, long gameId, string from, string to);
    }
}
