using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChessCommons
{
    public abstract class Figure
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public FigureColor Color { get; private set; }
        public ChessTable Table { get; private set; }
        public bool HasMoved { get; private set; }

        public Figure(FigureColor color, ChessTable table, int x, int y)
        {
            this.Color = color;
            this.Table = table;
            this.X = x;
            this.Y = y;
        }

        public List<Tuple<int, int>> GetPossibleMoves()
        {
            var moves = new List<Tuple<int, int>>();
            CalculatePossibleMoves(moves);
            return moves;
        }

        protected abstract void CalculatePossibleMoves(List<Tuple<int, int>> moves);

        public bool MoveTo(int x, int y)
        {
            if (Table.MoveFigure(this, x, y))
            {
                HasMoved = true;
                return true;
            }
            return false;
        }
    }
}
