using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class ChessTable
    {
        public static readonly int SIZE = 8;

        private readonly Figure[,] fields;
        private readonly List<Figure> killed;

        public ChessTable()
        {
            fields = new Figure[SIZE, SIZE];
            killed = new List<Figure>(32);
            for (int i = 0; i < 8; ++i)
            {
                killed.Add(new Pawn(FigureColor.White, this, i, 1));
                killed.Add(new Pawn(FigureColor.Black, this, i, 6));
            }
            killed.Add(new Rook(FigureColor.White, this, 0, 0));
            killed.Add(new Rook(FigureColor.White, this, 7, 0));
            killed.Add(new Knight(FigureColor.White, this, 1, 0));
            killed.Add(new Knight(FigureColor.White, this, 6, 0));
            killed.Add(new Bishop(FigureColor.White, this, 2, 0));
            killed.Add(new Bishop(FigureColor.White, this, 5, 0));
            killed.Add(new Queen(FigureColor.White, this, 4, 0));
            killed.Add(new King(FigureColor.White, this, 3, 0));
            killed.Add(new Rook(FigureColor.Black, this, 0, 7));
            killed.Add(new Rook(FigureColor.Black, this, 7, 7));
            killed.Add(new Knight(FigureColor.Black, this, 1, 7));
            killed.Add(new Knight(FigureColor.Black, this, 6, 7));
            killed.Add(new Bishop(FigureColor.Black, this, 2, 7));
            killed.Add(new Bishop(FigureColor.Black, this, 5, 7));
            killed.Add(new Queen(FigureColor.Black, this, 4, 7));
            killed.Add(new King(FigureColor.Black, this, 3, 7));
            foreach (var figure in killed)
            {
                fields[figure.X, figure.Y] = figure;
            }
            killed.Clear();
        }

        internal bool MoveFigure(Figure figure, int x, int y)
        {
            if (fields[figure.X, figure.Y] != figure)
            {
                return false;
            }
            var destFigure = fields[x, y];
            if (destFigure == null || destFigure.Color != figure.Color)
            {
                killed.Add(destFigure);
                fields[figure.X, figure.Y] = null;
                fields[x, y] = figure;
                figure.X = x;
                figure.Y = y;
                return true;
            }
            return false;
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
