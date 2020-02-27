using ChessCommons;
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
        Task<ChessPlayer> PlayerInfoAsync(long playerId);
        Task<ChessGameInfo> NewGameAsync(long playerId, PieceColor playerColor);
        Task<ChessGameInfo> JoinGameAsync(long playerId, long gameId);
        Task<ChessGameState> GameStateAsync(long gameId);
        Task<List<string>> ListPieceMovesAsync(long gameId, string from);
        Task<ChessGameState> MovePieceAsync(long playerId, long gameId, string from, string to);
    }
}
