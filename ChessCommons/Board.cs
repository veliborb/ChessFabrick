using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChessCommons
{
    public class Board
    {
        public static readonly int SIZE = 8;

        private readonly Figure[,] fields;
        private readonly List<Figure> alive;
        private readonly List<Figure> killed;
        public FigureMove LastMove { get; private set; }
        public FigureColor TurnColor { get; private set; } = FigureColor.White;

        public Board()
        {
            fields = new Figure[SIZE, SIZE];
            alive = new List<Figure>(32);
            killed = new List<Figure>(30);
            for (int i = 0; i < SIZE; ++i)
            {
               alive.Add(new Pawn(FigureColor.White, this, i, 1));
               alive.Add(new Pawn(FigureColor.Black, this, i, 6));
            }
            alive.Add(new Rook(FigureColor.White, this, 0, 0));
            alive.Add(new Rook(FigureColor.White, this, 7, 0));
            alive.Add(new Knight(FigureColor.White, this, 1, 0));
            alive.Add(new Knight(FigureColor.White, this, 6, 0));
            alive.Add(new Bishop(FigureColor.White, this, 2, 0));
            alive.Add(new Bishop(FigureColor.White, this, 5, 0));
            alive.Add(new Queen(FigureColor.White, this, 4, 0));
            alive.Add(new King(FigureColor.White, this, 3, 0));
            alive.Add(new Rook(FigureColor.Black, this, 0, 7));
            alive.Add(new Rook(FigureColor.Black, this, 7, 7));
            alive.Add(new Knight(FigureColor.Black, this, 1, 7));
            alive.Add(new Knight(FigureColor.Black, this, 6, 7));
            alive.Add(new Bishop(FigureColor.Black, this, 2, 7));
            alive.Add(new Bishop(FigureColor.Black, this, 5, 7));
            alive.Add(new Queen(FigureColor.Black, this, 4, 7));
            alive.Add(new King(FigureColor.Black, this, 3, 7));
            foreach (var figure in alive)
            {
                fields[figure.X, figure.Y] = figure;
            }
        }

        public List<Figure> GetAlive(FigureColor? color = null)
            => alive.FindAll(figure => color == null || figure.Color == color);

        public List<Figure> GetKilled(FigureColor? color = null)
            => killed.FindAll(figure => color == null || figure.Color == color);

        internal bool MoveFigure(Figure figure, int x, int y)
        {
            if (fields[figure.X, figure.Y] != figure || figure.Color != TurnColor)
            {
                return false;
            }

            var destFigure = fields[x, y];
            if (figure is Pawn)
            {
                if (destFigure == null && figure.X != x)
                {
                    destFigure = LastMove.MovedFigure;
                }
            }
            var rokada = (figure as King)?.Rokada(x, y);

            LastMove = new FigureMove(figure, x, y, destFigure, LastMove, rokada != null);

            if (destFigure != null)
            {
                fields[destFigure.X, destFigure.Y] = null;
                alive.Remove(destFigure);
                killed.Add(destFigure);
            }

            fields[figure.X, figure.Y] = null;
            figure.X = x;
            figure.Y = y;
            figure.HasMoved = true;
            fields[x, y] = figure;

            if ((rokada == null && CheckCheck(figure.Color.Other()).Count > 0)
                || (rokada != null && !MoveFigure(rokada.Item1, rokada.Item2, rokada.Item3)))
            {
                UndoMove();
                return false;
            }

            TurnColor = figure.Color.Other();
            return true;
        }

        public void UndoMove()
        {
            if (LastMove == null)
            {
                return;
            }

            fields[LastMove.FromX, LastMove.FromY] = LastMove.MovedFigure;
            LastMove.MovedFigure.X = LastMove.FromX;
            LastMove.MovedFigure.Y = LastMove.FromY;
            LastMove.MovedFigure.HasMoved = LastMove.FigureHasMoved;

            fields[LastMove.ToX, LastMove.ToY] = null;
            if (LastMove.KilledFigure != null)
            {
                fields[LastMove.KilledFigure.X, LastMove.KilledFigure.Y] = LastMove.KilledFigure;
                killed.Remove(LastMove.KilledFigure);
                alive.Add(LastMove.KilledFigure);
            }

            LastMove = LastMove.LastMove;
            if (LastMove?.ConnectedMove ?? false)
            {
                UndoMove();
            }
            else
            {
                TurnColor = LastMove?.MovedFigure?.Color.Other() ?? FigureColor.White;
            }
        }

        public List<Figure> CheckCheck(FigureColor color)
        {
            var checkFigures = new List<Figure>();
            var figures = GetAlive(color);
            foreach (var figure in figures)
            {
                if (figure.GetPossibleMoves().Find(field => fields[field.Item1, field.Item2] is King) != null)
                {
                    checkFigures.Add(figure);
                }
            }
            return checkFigures;
        }

        public bool CheckCheckmate(FigureColor color)
        {
            var figures = GetAlive(color.Other());
            foreach (var figure in figures)
            {
                foreach (var move in figure.GetPossibleMoves())
                {
                    figure.MoveTo(move.Item1, move.Item2);
                    var check = CheckCheck(color);
                    UndoMove();
                    if (check.Count == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool FieldExists(int x, int y)
        {
            return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
        }

        public Figure this[int x, int y]
        {
            get => fields[x, y];
        }
    }
}
