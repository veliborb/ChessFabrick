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
        Task<ChessGame> NewGameAsync();
        Task<ChessGame> GameInfoAsync(long gameId);
        Task<List<string>> ListPieceMovesAsync(long gameId, string from);
        Task<ChessGame> MovePieceAsync(long gameId, string from, string to);
    }
}
