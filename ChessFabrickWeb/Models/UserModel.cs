using ChessFabrickCommons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessFabrickWeb.Models
{
    public class UserModel
    {
        public ChessPlayer Player { get; set; }
        public string Token { get; set; }
    }
}
