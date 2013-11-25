using MazeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace MyWPFMaze
{
    class WPFMaze : VMaze
    {

        public double StrokeThicknessSquare { get; set; }
        public Color ColorSquare { get; set; }
        public double SquareSize { get; set; }

        public double OffsetX { get; set; }
        public double OffsetY { get; set; }

        private int _lineCounter;
        private Canvas Canvas_Paper { get; set; }
        private int CursorX { get; set; }
        private int CursorY { get; set; }
        private double Stroke { get; set; }
        private Color Color { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WPFMaze(Canvas canvas, int width, int height)
            : base(width, height)
        {
            Canvas_Paper = canvas;
            _lineCounter = 0;
            CursorX = 1;
            CursorY = 1;


            ColorSquare = Colors.Purple;
            StrokeThicknessSquare = 5;
            SquareSize = 48;

            OffsetX = 25;
            OffsetY = 15;
        }

        /// <summary>
        /// PrintLConerBase
        /// </summary>
        public override void PrintLConerBase()
        {
            //System.Console.Write("+--");

            Stroke = StrokeThicknessSquare;
            Color = ColorSquare;
            double X = XToS(CursorX);
            double Y = YToS(CursorY);
            DrawLine(X + Stroke / 2, Y - SquareSize, X - SquareSize - Stroke / 2, Y - SquareSize);
            CursorX++;
        }

        /// <summary>
        /// PrintLConer
        /// </summary>
        public override void PrintLConer()
        {
            //System.Console.Write("+  ");

            CursorX++;
        }

        /// <summary>
        /// PrintRConer
        /// </summary>
        public override void PrintRConer()
        {
            //System.Console.Write("+");

        }

        /// <summary>
        /// PrintWall
        /// </summary>
        public override void PrintWall()
        {
            //System.Console.Write("|");

            Stroke = StrokeThicknessSquare;
            Color = ColorSquare;
            double X = XToS(CursorX);
            double Y = YToS(CursorY);
            DrawLine(X - SquareSize, Y - SquareSize, X - SquareSize, Y);
            CursorX++;
        }

        /// <summary>
        /// PrintGap
        /// </summary>
        public override void PrintGap()
        {
            //System.Console.Write(" ");

            CursorX++;
        }

        /// <summary>
        /// PrintBoubleGap
        /// </summary>
        public override void PrintDoubleGap()
        {
            //System.Console.Write("  ");

        }

        /// <summary>
        /// NextLine
        /// </summary>
        public override void NextLine()
        {
            //System.Console.Write("\n");

            CursorX = 1;
            if (++_lineCounter % 2 == 0)  // only every second time!
                CursorY++;
        }

        /// <summary>
        /// DrawLine
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            Line Line = new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 };
            Line.Stroke = new SolidColorBrush(Color);
            Line.StrokeThickness = Stroke;
            Canvas_Paper.Children.Add(Line);
        }

        /// <summary>
        /// Transform X coordinate to sreen
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double XToS(double x)
        {
            return x * SquareSize + OffsetX;
        }

        /// <summary>
        /// Transform Y coordinate to sreen
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private double YToS(double y)
        {
            return y * SquareSize + OffsetY;
        }
    }
}
