using ChessFabrickCommons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessFabrickCommons.Models
{
    public class UserModel
    {
        public ChessPlayer Player { get; set; }
        public string Token { get; set; }
    }
}
