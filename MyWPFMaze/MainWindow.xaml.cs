/******************************************************************************/
/*                                                                            */
/*   Program: MyWPFMaze                                                       */
/*   A nice maze for WPF                                                      */
/*                                                                            */
/*   10.11.2013 0.0.0.0 uhwgmxorg Start                                       */
/*                                                                            */
/******************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tools;

namespace MyWPFMaze
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private double Stroke { get; set; }
        private Color Color { get; set; }

        private double StrokeThicknessBorder { get; set; }
        private Color ColorBorder { get; set; }

        private double MazeSize { get; set; }
        private int MazeSizeX { get; set; }
        private int MazeSizeY { get; set; }
        private Color MazeColorSquare = Colors.Purple;
        private double MazeStrokeThicknessSquare = 5;
        private double MazeSquareSize = 48;
        private double MazeOffsetX = 25;
        private double MazeOffsetY = 15;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Grid_Main.Visibility = System.Windows.Visibility.Visible;

            MazeSize = 3;
            Slider_MazeSize.Value = MazeSize;
            Init();
        }

        /// <summary>
        /// Init
        /// </summary>
        private void Init()
        {
            ColorBorder = Colors.Gray;
            StrokeThicknessBorder = 5;

            MazeSizeX = 4 * (int)Math.Round(MazeSize);
            MazeSizeY = 5 * (int)Math.Round(MazeSize);
            MazeSquareSize = 92 / MazeSize;

            MazeColorSquare = Colors.Purple;
            MazeStrokeThicknessSquare = 14/MazeSize;
            MazeOffsetX = 35;
            MazeOffsetY = 20;
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button_Start_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_New_Click(object sender, RoutedEventArgs e)
        {
            Canvas_Paper.Children.Clear();

            int MazeWidth = MazeSizeX;
            int MazeHeight = MazeSizeY;

            WPFMaze Maze = new WPFMaze(Canvas_Paper, MazeSizeX, MazeSizeY);

            Maze.ColorSquare = MazeColorSquare;
            Maze.StrokeThicknessSquare = MazeStrokeThicknessSquare;
            Maze.SquareSize = MazeSquareSize;
            Maze.OffsetX = MazeOffsetX;
            Maze.OffsetY = MazeOffsetY;
            Maze.Generat((int)RDNumber(0, MazeWidth - 1), (int)RDNumber(0, MazeHeight - 1));
            Maze.BuildExitSouth(1);

            Maze.Show();
        }

        /// <summary>
        /// Button_Clear_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Canvas_Paper.Children.Clear();
        }

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// Slider_MazeSize_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_MazeSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Slider_MazeSize != null)
            {
                MazeSize = (int)Slider_MazeSize.Value;
                Init();
            }
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// DrawBorder
        /// </summary>
        void DrawBorder()
        {
            Color = ColorBorder;
            Stroke = StrokeThicknessBorder;
            Canvas_Paper.Width = 436;
            Canvas_Paper.Height = 510;
            DrawLine(0 - Stroke / 2, 0, Canvas_Paper.Width + Stroke / 2, 0);
            DrawLine(Canvas_Paper.Width, 0, Canvas_Paper.Width, Canvas_Paper.Height);
            DrawLine(Canvas_Paper.Width + Stroke / 2, Canvas_Paper.Height, 0 - Stroke / 2, Canvas_Paper.Height);
            DrawLine(0, Canvas_Paper.Height, 0, 0);
        }

        /// <summary>
        /// DrawLine
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        void DrawLine(double x1, double y1, double x2, double y2)
        {
            Line Line = new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 };
            Line.Stroke = new SolidColorBrush(Color);
            Line.StrokeThickness = Stroke;
            Canvas_Paper.Children.Add(Line);
        }

        /// <summary>
        /// RDNumber
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        double RDNumber(double min,double max)
        {
            return Tools.StaticTools.RandomDouble(min,max);
        }

        #endregion
    }

    /******************************/
    /*     Converter Classes      */
    /******************************/
    public class DoubleToStringConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (int)(double)value;
            return (string)d.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double ret = System.Convert.ToDouble((string)value);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
