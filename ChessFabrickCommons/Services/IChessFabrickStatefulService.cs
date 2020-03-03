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
        Task<ChessGameInfo> NewGameAsync(string gameId, string playerName, PieceColor playerColor);
        Task<ChessGameInfo> JoinGameAsync(string gameId, string playerName);
        Task<List<string>> NewGameIdsAsync();
        Task<List<string>> ActiveGameIdsAsync();
        Task<List<string>> CompletedGameIdsAsync();
        Task<ChessGameState> GameStateAsync(string gameId);
        Task<List<string>> ListPieceMovesAsync(string gameId, string from);
        Task<ChessGameState> MovePieceAsync(string gameId, string playerName, string from, string to);
    }
}
