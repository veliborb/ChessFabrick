using ChessFabrickCommons.Models;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessFabrickCommons
{
    public interface IChessFabrickStatefulService : IService
    {
        Task<string> HelloChessAsync();

        Task<ChessGame> NewGameAsync();
    }
}
