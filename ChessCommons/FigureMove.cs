using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChessCommons
{
    public class FigureMove
    {
        public Figure MovedFigure { get; private set; }
        public bool FigureHasMoved { get; private set; }
        public int FromX { get; private set; }
        public int FromY { get; private set; }
        public int ToX { get; private set; }
        public int ToY { get; private set; }
        public Figure KilledFigure { get; private set; }
        public FigureMove LastMove { get; private set; }
        public bool ConnectedMove { get; private set; }

        public FigureMove(Figure movedFigure, int toX, int toY, Figure killedFigure = null, FigureMove lastMove = null, bool connectedMove = false)
        {
            MovedFigure = movedFigure;
            FromX = movedFigure.X;
            FromY = movedFigure.Y;
            FigureHasMoved = movedFigure.HasMoved;
            ToX = toX;
            ToY = toY;
            KilledFigure = killedFigure;
            LastMove = lastMove;
            ConnectedMove = connectedMove;
        }
    }
}
