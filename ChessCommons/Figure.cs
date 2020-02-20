﻿using System;
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
        public Board Board { get; private set; }
        public bool HasMoved { get; internal set; }

        public Figure(FigureColor color, Board board, int x, int y, bool hasMoved = false)
        {
            this.Color = color;
            this.Board = board;
            this.X = x;
            this.Y = y;
            this.HasMoved = hasMoved;
        }

        public Figure(Figure figure)
        {
            CopyFrom(figure);
        }

        public void CopyFrom(Figure figure)
        {
            X = figure.X;
            Y = figure.Y;
            Color = figure.Color;
            Board = figure.Board;
            HasMoved = figure.HasMoved;
        }

        public Figure Clone()
        {
            return MemberwiseClone() as Figure;
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
            if (!GetPossibleMoves().Contains(Tuple.Create(x, y)))
            {
                return false;
            }
            return Board.MoveFigure(this, x, y);
        }
    }
}
