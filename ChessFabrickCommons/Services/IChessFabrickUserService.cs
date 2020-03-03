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
    public interface IChessFabrickUserService : IService
    {
        Task<ChessPlayer> NewPlayerAsync(string name);
        Task<ChessPlayer> PlayerInfoAsync(string playerName);
    }
}
